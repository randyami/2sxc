﻿{
  "Note To Translators": {
    "1": "You probably don't want to translate this",
    "2": "Because it's mostly technical and these users will probably be fine with English. ",
    "3": "But if you do translate it, great! Just remember that it means more maintenance"
  },
  "SourceEditorSnippets": {
    "StandardFields": {
      "EntityId.Help": "the id as number of the current entity (content-item)",
      "EntityTitle.Help": "the title of the current entity (content-item) based on the content-type configuration",
      "EntityGuid.Help": "the guid-id of the current entity (content-item)",
      "EntityType.Help": "the type name like 'Person' or 'SimpleContent'",
      "IsPublished.Help": "true/false if this information is published - public user only see published content",
      "Modified.Help": "internal information when this content-item was last modified"
    },
    "Content": {
      "Title": "Content and Content-Presentation",
      "Help": "the most common data placeholder in a template",

      "General": {
        "Title": "General placeholders",
        "Help": "various common placeholders",
        "Toolbar.Key": "Toolbar",
        "Toolbar.Help": "Toolbar for inline editing with 2sxc. If used inside a <div class=\"sc-element\"> then the toolbar will automatically float",
        "ToolbarFloat.Key": "Toolbar floating",
        "ToolbarFloat.Help": "toolbar together with the <div> tag in which it floats"
      },

      "Fields.Title": "Fields",
      "Fields.Help": "fields of the content item as configured in the content-type",

      "PresentationFields.Title": "Presentation fields",
      "PresentationFields.Help": "presentation settings as configured in the content-type of presentation"
    },


    "List": {
      "Title": "List and List-Presentation",
      "Help": "list functionality in this template - if lists are enabled",

      "Header": {
        "Title": "Header general",
        "Help": "this is the header of a list",

        "Toolbar.Key": "Header toolbar",
        "Toolbar.Help": "Outputs the toolbar to edit list information - place in a <div> to float like other toolbars"
      },

      "Fields.Title": "List fields",
      "Fields.Help": "fields of the header content-item",

      "PresentationFields.Title": "List presentation fields",
      "PresentationFields.Help": "list presentation settings - usually for settings like paging-size, show-intro, etc.",

      "Repeaters": {
        "Title": "Repeaters",
        "Help": "placeholders as well as loop-templates and more",

        "Repeater.Help": "Allows defining the repeating part of the template."
      },
      "LoopItems": {
        "Title": "Loop Items (inside a repeater)",
        "Help": "placeholders for things inside a repeater",

        "Index.Help": "Index of the current item",
        "Index1.Help": "Index of the current item + 1 (for numbering lists)",
        "Count.Help": "Count of items in the list",
        "IsFirst.Help": "Outputs 'First' if current item is the first one",
        "IsLast.Help": "Outputs 'Last' if current item is the last one",
        "Alternator2.Help": "Outputs 0 or 1 depending on items index",
        "Alternator3.Help": "Outputs 0, 1 or 2 depending on items index",
        "Alternator4.Help": "Outputs 0, 1, 2 or 3 depending on items index",
        "Alternator5.Help": "Outputs 0, 1, 2, 3 or 4 depending on items index"
      },

      "Sets": [
        {
          "Id": "list",
          "Title": "List Header"

        },

        {
          "Title": "Important List Codes",
          "Items": [
            { "<repeat repeat=\"Employee in Data:Default\">...[Employee:...]...</repeat>": "Allows defining the repeating part of the template." },
            { "[Content:Repeater:Index]": "Index of the current item" },
            { "[Content:Repeater:Index1]": "Index of the current item + 1 (for numbering lists)" },
            { "[Content:Repeater:Count]": "Count of items in the list" },
            { "[Content:Repeater:IsFirst]": "Outputs <b>First<\/b> if current item is the first one" },
            { "[Content:Repeater:IsLast]": "Outputs <b>Last<\/b> if current item is the last one" },
            { "[Content:Repeater:Alternator2]": "Outputs <b>0<\/b> or <b>1<\/b> depending on items index" },
            { "[Content:Repeater:Alternator3]": "Outputs <b>0<\/b>, <b>1<\/b> or <b>2<\/b> depending on items index" },
            { "[Content:Repeater:Alternator4]": "Outputs <b>0<\/b>, <b>1<\/b>, <b>2<\/b> or <b>3<\/b> depending on items index" },
            { "[Content:Repeater:Alternator5]": "Outputs <b>0<\/b>, <b>1<\/b>, <b>2<\/b>, <b>3<\/b> or <b>4<\/b> depending on items index" },
            { "more...": "<a href&#61;\"http:\/\/2sxc.org\/help\">Learn more<\/a>=More help on 2sxc.org\/help" }
          ]
        },
        {
          "Id": "listpresentation",
          "Title": "List Presentation"
        }
      ]

    },

    "App": {
      "Title": "App",
      "Help": "App fields and placeholders",
      "General": {
        "Title": "General",
        "Help": "General App placeholders",


        "old": {
          "Path": "returns the url to the current app, for integrating scripts, images etc. For example, use as ***\/scripts\/knockout.js",
          "PhysicalPath": "physical path, in c:\\",
          "AppGuid": "internal GUID - should stay the same across all systems for this specific App",
          "AppId": "Id in the current data base. Is a different number in every App-Installation",
          "Name": "internal name",
          "Folder": "folder of the 2sxc-app"
        }
      },

      "Resources.Title": "App Resources",
      "Resources.Help": "from the app resources content-type which can be configured in the app tab of the app-dialog - usually multi-language",
      "Settings.Title": "App Settings",
      "Settings.Help": "from the app settings content-type which can be configured in the app tab of the app-dialog - usually single-language but can be multi-language"
    },

    "DnnToken": {
      "Title": "Dnn (Portal, Tab, Module, ...)",
      "Help": "every dnn-token like Portal, Tab, Module, etc.",

      "Portal": {
        "Title": "Portal Tokens",
        "Currency.Help": "Currency String",
        "Description.Help": "Portal Description",
        "Email.Help": "Portal Admin Email",
        "FooterText.Help": "Portal Copyright Text",
        "HomeDirectory": "Portal Path (relative) of Home Directory",
        "LogoFile": "Portal Path to Logo File",
        "PortalName": "Portal Name",
        "PortalAlias": "Portal URL",
        "TimeZoneOffset": "Difference in Minutes between Portal Default Time and UTC"
      },





      "todo": "continue-here",

      "TabTokens": {
        "List": "[Tab:Description]=Page Description Text for Search Engine \n[Tab:EndDate]=Page Display Until Date \n[Tab:FullUrl]=Page Full URL \n[Tab:IconFile]=Page Relative Path to Icon File \n[Tab:KeyWords]=Page Keywords for Search Engine \n[Tab:PageHeadText]=Page Header Text \n[Tab:StartDate]=Page Display from Date \n[Tab:TabName]=Page Name \n[Tab:TabPath]=Page Relative Path \n[Tab:Title]=Page Title (Window Title) \n[Tab:URL]=Page URL",
        "Title": "Tab (Page) Tokens"
      },

      "ModuleTokens": {
        "List": "[Module:Description]=Module Definition Description \n[Module:EndDate]=Module Display Until Date \n[Module:Footer]=Module Footer Text \n[Module:FriendlyName]=Module Definition Name \n[Module:Header]=Module Header Text \n[Module:HelpURL]=Module Help URL \n[Module:IconFile]=Module Path to Icon File \n[Module:ModuleTitle]=Module Title \n[Module:PaneName]=Module Name of Pane (where the module resides) \n[Module:StartDate]=Module Display from Date",
        "Title": "Module Tokens"
      }
    },
    "DnnRazor": {
      "Title": "Dnn objects (module, tab, portal, etc.)",
      "Help": "find more on <a href='http://2sxc.org/en/Docs-Manuals/Feature/feature/3602'>token list</a>",
      "Module.Title": "Module",

      "Tab.Title": "Tab (page)",
      "Tab.EndDate.Help": "for automatic hiding of the page",

      "Portal.Title": "Portal",
      "Portal.Current.Help": "static method returning the current portal portal-settings",
      "Portal.Default.Help": "(AdminContainer, AdminSkin, ControlPanelMode, etc.)",
      "Portal.HomeTabId.Help": "the root page",
      "Portal.LoginTabId.Help": "where the normal login usually goes to",
      "Portal.Search.Help": "various values like IncludeCommon, IncludeTagInfoFilter etc."


    },
    "DnnRazorUser": {
      "Title": "Dnn user information",
      "User.Title": "User",
      "User.Membership.Help": "these are asp.net memberships, you probably don't need them",
      "User.Roles.Help": "these are DNN roles",
      "Profile.Title": "Profile",
      "Profile.Help": "profile information as configured in your portal, like city etc.",
      "Profile.AnyValue.Help": "any property of the user profile as configured in your portal"
    },

    "ProfileTokens": {
      "List": "[Profile:]=Use any default or custom Profile Property as listed in Profile Property Definition section of Manage User Accounts. Use non-localized Property Name only.",
      "Title": "User Profile Tokens"
    },
    "QueryStringTokens": {
      "List": "[Querystring:Name]=Value of Querystring Name",
      "Title": "QueryString (URL-Parameters) Tokens"
    },

    "TimeTokens": {
      "List": "[DateTime:Now]=Current Date and Time \n[Ticks:Now]=CPU Tick Count for Current Second \n[Ticks:Today]=CPU Tick Count since Midnight \n[Ticks:TicksPerDay]=CPU Ticks per Day (for calculations)",
      "Title": "Time Tokens"
    },
    "UserTokens": {
      "List": "[User:DisplayName]=User’s Display Name \n[User:Email]=User’s Email Address \n[User:FirstName]=User’s First Name \n[User:FullName]=(deprecated)\n[User:LastName]=User’s Last Name \n[User:Username]=User’s Login User Name",
      "Title": "User Tokens"
    }
  }
  }