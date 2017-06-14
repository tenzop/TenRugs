var tenRugs = {
    utilities: {}
    , layout: {}
    , page: {
        handlers: {
        }
        , startUp: null
    }
    , services: {}
    , ui: {}
};

tenRugs.moduleOptions = {
    APPNAME: "tenRugsApp"
    , extraModuleDependencies: []
    , runners: []
    , page: tenRugs.page//we need this object here for later use
};

tenRugs.layout.startUp = function () {

    console.debug("tenRugs.layout.startUp");

    //this does a null check on sabio.page.startUp
    if (tenRugs.page.startUp) {
        console.debug("tenRugs.page.startUp");
        tenRugs.page.startUp();
    }
};

tenRugs.APPNAME = "tenRugsApp";//legacy
$(document).ready(tenRugs.layout.startUp);