﻿using DotNetNuke.Entities.Portals;
using DotNetNuke.Services.FileSystem;
using ToSic.Eav.Apps;
using ToSic.Eav.Apps.ImportExport;
using ToSic.Eav.ImportExport.Environment;
using ToSic.Eav.Logging.Simple;
using ToSic.Sxc.Adam;
using ToSic.SexyContent.Environment.Dnn7;

namespace ToSic.SexyContent.ImportExport
{
    public class DnnXmlExporter: XmlExporter
    {
        private readonly IFileManager _dnnFiles = FileManager.Instance;
        internal AdamAppContext AdamAppContext;

        public override XmlExporter Init(int zoneId, int appId, AppRuntime appRuntime, bool appExport, string[] attrSetIds, string[] entityIds, Log parentLog)
        {
            var tenant = new DnnTenant(PortalSettings.Current);
            var app = new App(tenant, zoneId, appId);
            AdamAppContext = new AdamAppContext(tenant, app, null, Log);
            Constructor(zoneId, appRuntime, app.AppGuid, appExport, attrSetIds, entityIds, parentLog);

            // this must happen very early, to ensure that the file-lists etc. are correct for exporting when used externally
            InitExportXDocument(PortalSettings.Current.DefaultLanguage, Settings.ModuleVersion);

            return this;
        }

        public override void AddFilesToExportQueue()
        {
            // Add Adam Files To Export Queue
            var adamIds = AdamAppContext.Export.AppFiles;
            adamIds.ForEach(AddFileAndFolderToQueue);

            // also add folders in adam - because empty folders may also have metadata assigned
            var adamFolders = AdamAppContext.Export.AppFolders;
            adamFolders.ForEach(ReferencedFolderIds.Add);
        }

        protected override void AddFileAndFolderToQueue(int fileNum)
        {
            try
            {
                ReferencedFileIds.Add(fileNum);

                // also try to remember the folder
                try
                {
                    var file = _dnnFiles.GetFile(fileNum);
                    ReferencedFolderIds.Add(file.FolderId);
                }
                catch
                {
                    // don't do anything, because if the file doesn't exist, its FOLDER should also not land in the queue
                }
            }
            catch
            {
                // don't do anything, because if the file doesn't exist, it should also not land in the queue
            }
        }

        protected override string ResolveFolderId(int folderId)
        {
            var folderController = FolderManager.Instance;
            var folder = folderController.GetFolder(folderId);
            return folder?.FolderPath;
        }

        protected override TenantFileItem ResolveFile(int fileId)
        {
            var fileController = FileManager.Instance;
            var file = fileController.GetFile(fileId);
            return new TenantFileItem
            {
                Id = fileId,
                RelativePath = file?.RelativePath.Replace('/', '\\'),
                Path = file?.PhysicalPath
            };
        }

    }
}