function RecipeModel(params) {
    var self = this;

    self.recipes = ko.observableArray([]);
    self.foodCategories = ko.observableArray([]);




    self.getRecipes = function() {
        $.get("/recipe/getRecipes",
            function (data) {
                for (var i = 0; i < data.length; i++) {
                    self.foodCategories.push({
                        recipes: data[i].Recipes,
                        name: data[i].Name,
                        id: data[i].Id
                    });
                }
            });
    }

    self.getRecipesByCategoryId = function(data) {
        self.recipes([]);
        for (var i = 0; i < data.recipes.length; i++) {
            self.recipes.push({
                id: data.recipes[i].Id,
                name: data.recipes[i].Name,
                text: data.recipes[i].Description,
                image: data.recipes[i].ImageUrl
            });
        }
    }
    self.getAllRecipes = function() {
        self.recipes([]);
        $.each(self.foodCategories(), function(index, value) {
            for (var i = 0; i < value.recipes.length; i++) {
                self.recipes.push({
                    id: value.recipes[i].Id,
                    name: value.recipes[i].Name,
                    text: value.recipes[i].Description,
                    image: value.recipes[i].ImageUrl
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