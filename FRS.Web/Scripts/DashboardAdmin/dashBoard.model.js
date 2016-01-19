define(["ko", "underscore", "underscore-ko"], function(ko) {
    
    //Region Detail
    var dashBoard = function (specifiedId, spcLogoUrl, spcDisName, spcDisNameAr, spcSlogan, spcSloganAr, spcBan1, spcBan2, spcBan3, spcServices, spcServicesAr,spcAboutus,spcAboutusAr, spcAddress,
    spcAddressAr,spcTelephone, spcFbLink, spcTwitter, spcBgColor, spcEmail, spcShortname, spcIcon) {
        var            
            id = ko.observable(specifiedId),
            logoUrl = ko.observable(spcLogoUrl),
            companyDisplayName = ko.observable(spcDisName).extend({  }),
            companyDisplayNameAr = ko.observable(spcDisNameAr).extend({}),
            slogan = ko.observable(spcSlogan),
            sloganAr = ko.observable(spcSloganAr),
            banner1Url = ko.observable(spcBan1).extend({ }),
            banner2Url = ko.observable(spcBan2),
            
            banner3Url = ko.observable(spcBan3).extend({ }),
            servicesAr = ko.observable(spcServicesAr),
            services = ko.observable(spcServices),
            aboutUs = ko.observable(spcAboutus).extend({  }),
            aboutUsAr = ko.observable(spcAboutusAr).extend({}),
            address = ko.observable(spcAddress),
            addressAr = ko.observable(spcAddressAr),
            
            telephone = ko.observable(spcTelephone).extend({  }),
            fbLink = ko.observable(spcFbLink).extend({ }),
            twitterLink = ko.observable(spcTwitter).extend({  }),
            bgColor = ko.observable(spcBgColor),
            
            email = ko.observable(spcEmail).extend({email: true  }),
            shortName = ko.observable(spcShortname),
            
            iconUrl = ko.observable(spcIcon),
           
            errors = ko.validation.group({
                fbLink: fbLink,
                twitterLink: twitterLink,
                email: email
            }),
            // Is Valid
            isValid = ko.computed(function() {
                return errors().length === 0;
            }),
            dirtyFlag = new ko.dirtyFlag({
               
            }),
            // Has Changes
            hasChanges = ko.computed(function() {
                return dirtyFlag.isDirty();
            }),
            // Reset
            reset = function() {
                dirtyFlag.reset();
            },
            // Convert to server
            convertToServerData = function () {
         
                return {
                    SiteContentId: id(),
                    CompanyLogo: logoUrl(),
                    CompanyDisplayName: companyDisplayName(),
                    CompanyDisplayNameAr: companyDisplayNameAr(),
                    Slogan: slogan(),
                    SloganAr: sloganAr(),
                    Banner1: banner1Url(),
                    Banner2: banner2Url(),

                    Banner3: banner3Url(),
                    ServiceContents: services(),
                    ServiceContentsAr: servicesAr(),
                    AboutusContents: aboutUs(),
                    AboutusContentsAr: aboutUsAr(),
                    Address: address(),
                    AddressAr: addressAr(),

                    Telephone: telephone(),
                    FBLink: fbLink(),
                    TwiterLink: twitterLink(),
                    BodyBGColor: bgColor(),
                  

                    Email: email(),
                    CompanyShortName: shortName(),
                    IconImage: iconUrl()
                };
            };
        return {
            id :id,
            logoUrl :logoUrl,
            companyDisplayName: companyDisplayName,
            companyDisplayNameAr:companyDisplayNameAr,
            slogan: slogan,
            sloganAr:sloganAr,
            banner1Url :banner1Url,
            banner2Url :banner2Url,
            
            banner3Url: banner3Url,
            servicesAr:servicesAr,
            services :services,
            aboutUs :aboutUs,
            address:address,
            
            telephone:telephone,
            fbLink:fbLink,
            twitterLink :twitterLink,
            bgColor:bgColor,
            
            email:email,
            shortName: shortName,
            iconUrl: iconUrl,
            addressAr: addressAr,
            aboutUsAr: aboutUsAr,

            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset
            
        };
    };
    // server to client mapper
    var dashBoardServertoClinetMapper = function (source) {
        return dashBoard.Create(source);
    };
    
    // Region Factory
    dashBoard.Create = function (source) {
        return new dashBoard(source.SiteContentId, source.CompanyLogo, source.CompanyDisplayName, source.CompanyDisplayNameAr, source.Slogan, source.SloganAr, source.Banner1, source.Banner2Source, source.Banner3Source, source.ServiceContents, source.ServiceContentsAr,
            source.AboutusContents, source.AboutusContentsAr, source.Address, source.AddressAr, source.Telephone, source.FBLink, source.TwiterLink, source.BodyBGColor, source.Email, source.CompanyShortName,
        source.IconImage);
    };

  
    return {
        dashBoard: dashBoard,
        dashBoardServertoClinetMapper: dashBoardServertoClinetMapper,
    };
});