﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Controllers;
using ToSic.Eav.Apps.Interfaces;
using ToSic.Eav.DataSources;
using ToSic.Eav.Interfaces;
using ToSic.Eav.ValueProvider;
using ToSic.Sxc.Adam;
using ToSic.SexyContent.DataSources;
using ToSic.SexyContent.Environment.Dnn7;
using ToSic.SexyContent.Razor.Helpers;
using Factory = ToSic.Eav.Factory;
using ToSic.Sxc.Adam.WebApi;
using System.IO;
using ToSic.Eav.Configuration;
using File = ToSic.Sxc.Adam.File;

namespace ToSic.SexyContent.WebApi
{
    /// <inheritdoc cref="SxcApiControllerBase" />
    /// <summary>
    /// This is the base class for API Controllers which need the full context
    /// incl. the current App, DNN, Data, etc.
    /// For others, please use the SxiApiControllerBase, which doesn't have all that, and is usually then
    /// safer because it can't accidentally mix the App with a different appId in the params
    /// </summary>
    [SxcWebApiExceptionHandling]
    public abstract class SxcApiController : SxcApiControllerBase, IAppAndDataHelpers
    {
        #region constructor

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            // Note that the SxcInstance is created by the BaseClass, if it's detectable. Otherwise it's null
            DnnAppAndDataHelpers = new DnnAppAndDataHelpers(SxcInstance, SxcInstance?.Log ?? Log);
            controllerContext.Request.Properties.Add(Constants.DnnContextKey, Dnn); // must run after creating AppAndDataHelpers
        }
        #endregion

        private DnnAppAndDataHelpers DnnAppAndDataHelpers { get; set; }

        #region AppAndDataHelpers implementation

        public DnnHelper Dnn => DnnAppAndDataHelpers.Dnn;

        public SxcHelper Sxc => DnnAppAndDataHelpers.Sxc;

        /// <inheritdoc />
        public App App
        {
            get
            {
                // try already-retrieved
                if (_app != null)
                    return _app;

                // try "normal" case with instance context
                if (SxcInstance != null)
                    return _app = DnnAppAndDataHelpers.App;

                var routeAppPath = Request.GetRouteData().Values["apppath"]?.ToString();
                var appId = GetCurrentAppIdFromPath(routeAppPath).AppId;
                // Look up if page publishing is enabled - if module context is not availabe, always false
                var publish = Factory.Resolve<IEnvironmentFactory>().PagePublisher(Log);
                var publishingEnabled = Dnn.Module != null && publish.IsEnabled(Dnn.Module.ModuleID);
                return _app = (App) Environment.Dnn7.Factory.App(appId, publishingEnabled);
            }
        }
        private App _app;

        /// <inheritdoc />
        public ViewDataSource Data => DnnAppAndDataHelpers.Data;

	    /// <inheritdoc />
        public dynamic AsDynamic(IEntity entity) => DnnAppAndDataHelpers.AsDynamic(entity);


        /// <inheritdoc />
        public dynamic AsDynamic(dynamic dynamicEntity) =>  DnnAppAndDataHelpers.AsDynamic(dynamicEntity);

        /// <inheritdoc />
        public dynamic AsDynamic(KeyValuePair<int, IEntity> entityKeyValuePair) =>  DnnAppAndDataHelpers.AsDynamic(entityKeyValuePair.Value);

        /// <inheritdoc />
        public IEnumerable<dynamic> AsDynamic(IDataStream stream) =>  DnnAppAndDataHelpers.AsDynamic(stream.List);

        /// <inheritdoc />
        public IEntity AsEntity(dynamic dynamicEntity) =>  DnnAppAndDataHelpers.AsEntity(dynamicEntity);

        /// <inheritdoc />
        public IEnumerable<dynamic> AsDynamic(IEnumerable<IEntity> entities) =>  DnnAppAndDataHelpers.AsDynamic(entities);

	    public IDataSource CreateSource(string typeName = "", IDataSource inSource = null,
	        IValueCollectionProvider configurationProvider = null)
	        => DnnAppAndDataHelpers.CreateSource(typeName, inSource, configurationProvider);

        public T CreateSource<T>(IDataSource inSource = null, IValueCollectionProvider configurationProvider = null)
            =>  DnnAppAndDataHelpers.CreateSource<T>(inSource, configurationProvider);

	    /// <inheritdoc />
	    public T CreateSource<T>(IDataStream inStream) => DnnAppAndDataHelpers.CreateSource<T>(inStream);

        /// <summary>
        /// content item of the current view
        /// </summary>
        public dynamic Content => DnnAppAndDataHelpers.Content;

        /// <summary>
        /// presentation item of the content-item. 
        /// </summary>
        [Obsolete("please use Content.Presentation instead")]
        public dynamic Presentation => DnnAppAndDataHelpers.Content?.Presentation;

	    public dynamic ListContent => DnnAppAndDataHelpers.ListContent;

        /// <summary>
        /// presentation item of the content-item. 
        /// </summary>
        [Obsolete("please use ListContent.Presentation instead")]
	    public dynamic ListPresentation => DnnAppAndDataHelpers.ListContent?.Presentation;

        [Obsolete("This is an old way used to loop things - shouldn't be used any more - will be removed in 2sxc v10")]
        public List<Element> List => DnnAppAndDataHelpers.List;

	    #endregion


        #region Adam

	    /// <summary>
	    /// Provides an Adam instance for this item and field
	    /// </summary>
	    /// <param name="entity">The entity, often Content or similar</param>
	    /// <param name="fieldName">The field name, like "Gallery" or "Pics"</param>
	    /// <returns>An Adam object for navigating the assets</returns>
	    public FolderOfField AsAdam(DynamicEntity entity, string fieldName)
	        => DnnAppAndDataHelpers.AsAdam(AsEntity(entity), fieldName);

        /// <summary>
        /// Provides an Adam instance for this item and field
        /// </summary>
        /// <param name="entity">The entity, often Content or similar</param>
        /// <param name="fieldName">The field name, like "Gallery" or "Pics"</param>
        /// <returns>An Adam object for navigating the assets</returns>
        public FolderOfField AsAdam(IEntity entity, string fieldName) => DnnAppAndDataHelpers.AsAdam(entity, fieldName);


        /// <summary>
        /// Save a file from a stream (usually an upload from the browser) into an adam-field
        /// </summary>
        /// <param name="stream">the stream</param>
        /// <param name="fileName">file name to save to</param>
        /// <param name="contentType">content-type of the target item (important for security checks)</param>
        /// <param name="guid"></param>
        /// <param name="field"></param>
        /// <param name="subFolder"></param>
        /// <returns></returns>
        public File SaveInAdam(string dontRelyOnParameterOrder = Constants.RandomProtectionParameter, 
            Stream stream = null, 
            string fileName = null, 
            string contentType = null, 
            Guid? guid = null, 
            string field = null,
            string subFolder = "")
        {
            Constants.ProtectAgainstMissingParameterNames(dontRelyOnParameterOrder, "SaveInAdam", 
                $"{nameof(stream)},{nameof(fileName)},{nameof(contentType)},{nameof(guid)},{nameof(field)},{nameof(subFolder)} (optional)");

            if(stream == null || fileName == null || contentType == null || guid == null || field == null)
                throw new Exception();

            var feats = new[]{FeatureIds.UseAdamInWebApi, FeatureIds.PublicUpload};
            if (!Features.EnabledOrException(feats, "can't save in ADAM", out var exp))
                throw exp;
            //if(!Features.Enabled(feats))
            //    throw new Exception(Features.MsgMissingSome(feats));

            return new AdamUploader(SxcInstance, 
                SxcInstance.AppId ?? throw new Exception("can't save in adam - full context not available"), 
                Log)
                .UploadOne(stream, fileName, contentType, guid.Value, field, subFolder, false, true);
        }

        #endregion





    }
}
