
function RecipeEditModel() {
    var Category = function (id, name, recipes) {
        this.id = id;
        this.name = ko.observable(name);
        this.recipes = recipes;
    }
    self.recipes = ko.observableArray([]);
    self.foodCategories = ko.observableArray([]);

    self.NewRecipeModel = function() {
        this.descriptionSteps = ko.observableArray([]);
        this.stepNubmerText = ' ШАГ';
        this.Step = function () {
            this.text = ko.observable("");
        }
        this.descriptionSteps.push({text:ko.observable()});
        this.addStep = function () {
            self.descriptionSteps.push({ text: ko.observable() });
        }
        this.removeStep = function (step) {
            self.descriptionSteps.remove(step);
        }
        this.newRecipeName = ko.observable();
        this.categoryId = ko.observable();
        this.image = ko.observable();
        this.image.subscribe(function (newValue) {
            debugger;
        });

        self.createRecipe = function (params) {
            debugger;
            var dstep = descriptionSteps();
            var arr = [];
            $.each(dstep, function() {
                arr.push(this.text());
            });
            $.post("/Admin/createRecipe",
                {
                    newRecipeName:newRecipeName,
                    categoryId: categoryId,
                    descriptionSteps: arr
                },
                function (result) {
                    debugger;
                });
        } 
    }
    


   

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