function RecipeModel(params) {
    
    var self = this;
    debugger;
   
    self.name = ko.observable(params.recipe.name);
    self.text = ko.observable(params.recipe.text);
    self.image = ko.observable(params.recipe.image);
    self.id = ko.observable(params.recipe.id);
    self.isClickRecipe = params.recipe.isClickRecipe;
    self.recipeComments = params.recipe.recipeComments;
    
    self.getCommentByRecipeId = function (recipeId) {
        $.post("/Recipe/GetCommentsByRecipeId", { recipeId: recipeId },
            function (data) {
                self.recipeComments([]);
                for (var i = 0; i < data.length; i++) {
                    self.recipeComments.push({
                        text: data[i].Text,
                        author: data[i].Author,
                        createDateTime: data[i].CreateDateTime,
                        status: data[i].Status
                    });
                }
            });
    }
    
   
    self.getRecipContent = function (recipe) {
        params.recipe.isClickRecipe(true);
        self.getCommentByRecipeId(recipe.id());
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