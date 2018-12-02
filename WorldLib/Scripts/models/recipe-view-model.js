function RecipeModel(params) {
    
    var self = this;
    
   
    self.name = ko.observable(params.recipe.name);
    self.text = ko.observable(params.recipe.text);
    self.image = ko.observable(params.recipe.image);
    self.id = ko.observable(params.recipe.id);
    self.recipeComments = params.recipe.recipeComments;
    self.isClickRecipe = params.recipe.isClickRecipe;
    
    self.getCommentByRecipeId = function (recipeId) {
        $.post("/Recipe/GetCommentsByRecipeId", { recipeId: recipeId },
            function (data) {
                //  // //
            });
    }
    
   
    self.getRecipContent = function (recipe) {
        debugger;
        self.getCommentByRecipeId(recipe.id());
        params.recipe.isClickRecipe(true);
    }

    self.createComment = function(recipe) {
        debugger;
    }
   
}

ko.components.register('recipe',
    {
        template: { element: 'recipe-template' },
        viewModel: RecipeModel
    });