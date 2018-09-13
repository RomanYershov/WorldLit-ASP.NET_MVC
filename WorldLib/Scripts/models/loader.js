define(['jquery', 'knockout'], function($, ko) {
    ko.components.register('testCommentsRequere', {
        require: 'Scripts/models/testCommentsRequere'
    });
    ko.applyBindings();
})