function CategoryModel(params) {
    var self = this;

    self.recipes = ko.observableArray([]);
    self.foodCategories = ko.observableArray([]);




    self.getRecipes = function () {
        $.get("/recipe/getRecipes",
            function (data) {
                for (var i = 0; i < data.length; i++) {
                    self.foodCategories.push({
                        recipes: data[i].Recipes,
                        name: data[i].Name,
                        id: data[i].Id
                    });
                }
                self.getAllRecipes();
            });
    }

    self.getRecipesByCategoryId = function (data) {
        self.recipes([]);
        for (var i = 0; i < data.recipes.length; i++) {
            self.recipes.push({
                id: data.recipes[i].Id,
                name: data.recipes[i].Name,
                text: data.recipes[i].Description,
                image: data.recipes[i].ImageUrl,
                isClickRecipe: ko.observable(false)
            });
        }
    }
    self.getAllRecipes = function () {
        self.recipes([]);
        $.each(self.foodCategories(), function (index, value) {
            for (var i = 0; i < value.recipes.length; i++) {
                self.recipes.push({
                    id: value.recipes[i].Id,
                    name: value.recipes[i].Name,
                    text: value.recipes[i].Description,
                    image: value.recipes[i].ImageUrl,
                    isClickRecipe: ko.observable(false)
                });
            }
        });

    }
    self.getRecipes();

   
    self.search = ko.observable();
    self.search.subscribe(function (value) {
        self.recipes([]);
        var newValue = value;
        $.each(self.foodCategories(), function (index, value) {
            for (var i = 0; i < value.recipes.length; i++) {
                var nameToUpperCase = value.recipes[i].Name.toUpperCase();
                if (nameToUpperCase.indexOf(newValue.toUpperCase()) != -1) {
                    self.recipes.push({
                        id: value.recipes[i].Id,
                        name: value.recipes[i].Name,
                        text: value.recipes[i].Description,
                        image: value.recipes[i].ImageUrl,
                        isClickRecipe: ko.observable(false)
                    });
                }
            }
        });
    });
 
    self.removeBlock = function (data) {
        data.isClickRecipe(false);
    }
}

ko.components.register('category',
    {
        template: { element: 'category-template' },
        viewModel: CategoryModel
    });