define(["knockout"],
    function(ko) {
        function AfforismDialog(params) {
            var self = this;

            self.text = ko.observable(params.title);


        }

        return new AfforismDialog();

    });