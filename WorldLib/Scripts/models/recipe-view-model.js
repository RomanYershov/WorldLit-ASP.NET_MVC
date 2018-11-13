function RecipeModel(params) {
    var self = this;

    self.recipes = ko.observableArray([]);





    self.getRecipes = function() {
        $.get("/recipe/getRecipes",
            function(data) {
                debugger;
                for (var i = 0; i < data.length; i++) {
                    self.recipes.push({
                        id: data[i].Recipe.Id,
                        name: data[i].Recipe.Name,
                        description: data[i].Recipe.Description,
                        image: data[i].Recipe.ImageUrl

                    });
                }
            });
    }

    self.getRecipes();


}

ko.components.register('recipes',
    {
        template: { element: 'recipe-template' },
        viewModel: RecipeModel
    });