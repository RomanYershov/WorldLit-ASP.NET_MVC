define(['knockout', 'text!../comments.html'],
    function(ko, commentsView) {
        function comments(params) {
            var self = this;
            self.firstName = ko.observable(params.firstName),
                self.lastName = 'Yershov'
            return self;
        }
        return { viewModel: comments, template: commentsView };
    });