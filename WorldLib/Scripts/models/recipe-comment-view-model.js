var RecipeComment = function(params) {
    var self = this;

    // self.comment = {
    //    text: ko.observable(params.comment.text),
    //    author : params.comment.author,
    //    createDateTime : ko.observable(new Date(parseInt(params.comment.createDateTime.match(/[0-9]+/)))),
    //    status : ko.observable(params.comment.status)
    //}
    

    return  function() {
        this.text = ko.observable(params.comment.text);
        this.author = params.comment.author;
        this.createDateTime = ko.observable(new Date(parseInt(params.comment.createDateTime.match(/[0-9]+/))));
        this.status = ko.observable(params.comment.status);
    }
    
}



ko.components.register('recipe-comment',
    {
        template: { element: 'recipe-comment-template' },
        viewModel: RecipeComment
    });