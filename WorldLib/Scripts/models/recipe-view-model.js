function RecipeModel(params) {
    
    var self = this;
    
   
    self.name = ko.observable(params.recipe.name);
    self.text = ko.observable(params.recipe.text);
    self.image = ko.observable(params.recipe.image);
    self.id = ko.observable(params.recipe.id);
    self.recipeComments = params.recipe.recipeComments;
    self.isClickRecipe = params.recipe.isClickRecipe;
    

    //ko.extenders.resipe = function(target, param) {
    //    target.isClickRecipe = ko.observable(param);
    //}
   
    self.getRecipContent = function(recipe) {
        var value = params;
        debugger;
        params.recipe.isClickRecipe(true);
        debugger;
    }

   
}

ko.components.register('recipe',
    {
        template: { element: 'recipe-template' },
        viewModel: RecipeModel
    });