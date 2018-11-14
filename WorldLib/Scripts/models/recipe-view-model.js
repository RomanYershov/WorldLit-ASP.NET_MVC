function RecipeModel(params) {
    var self = this;
    debugger;
    self.name = ko.observable(params.recipe.name);
    self.text = ko.observable(params.recipe.text);
    self.image = ko.observable(params.recipe.image);
}

ko.components.register('recipe',
    {
        template: { element: 'recipe-template' },
        viewModel: RecipeModel
    });