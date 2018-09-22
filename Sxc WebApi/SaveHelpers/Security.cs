﻿using System.Collections.Generic;
using System.Linq;
using ToSic.Eav.Logging.Simple;
using ToSic.Eav.Security.Permissions;
using ToSic.Eav.WebApi.Formats;
using ToSic.SexyContent.WebApi.Permissions;

namespace ToSic.SexyContent.WebApi.SaveHelpers
{
    internal class Security:SaveHelperBase
    {
        public Security(SxcInstance sxcInstance, Log parentLog) : base(sxcInstance, parentLog, "Api.SavSec") { }


        public IMultiPermissionCheck DoSaveSecurityCheck(int appId, IEnumerable<BundleWithHeader> items)
        {
            var permCheck = new MultiPermissionsTypes(SxcInstance, appId, items.Select(i => i.Header).ToList(), Log);
            if (!permCheck.EnsureAll(GrantSets.WriteSomething,  out var exp))
                throw exp;
            if (!permCheck.UserCanWriteAndPublicFormsEnabled(out exp))
                throw exp;
            Log.Add("passed security checks");
            return permCheck;

        }
    }
}