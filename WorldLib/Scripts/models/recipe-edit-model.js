
function RecipeEditModel() {
    var Category = function (id, name, recipes) {
        this.id = id;
        this.name = ko.observable(name);
        this.recipes = recipes;
    }
    self.recipes = ko.observableArray([]);
    self.foodCategories = ko.observableArray([]);


    self.getCategories = function () {
        $.get("/recipe/getCategories",
            function (data) {
                for (var i = 0; i < data.length; i++) {
                    self.foodCategories.push(new Category(data[i].Id, data[i].Name, data[i].Recipes));
                }
                self.getAllRecipes();
            });
    }
    self.getAllRecipes = function () {
        self.recipes([]);
        $.each(self.foodCategories(), function (index, value) {
            debugger;
            for (var i = 0; i < value.recipes.length; i++) {
                self.recipes.push({
                    id: value.recipes[i].Id,
                    name: value.recipes[i].Name,
                    text: value.recipes[i].Description,
                    image: value.recipes[i].ImageUrl,
                    categoryName: value.name
                });
            }
        });

    }
    self.getCategories();
}



ko.components.register('recipe',
    {
        template: { element: 'recipe-edit-template' },
        viewModel: RecipeEditModel
    });