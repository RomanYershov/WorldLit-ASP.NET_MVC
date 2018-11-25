﻿function CategoryModel(params) {
    var self = this;
    ko.extenders.required = function (target, overrideMessage) {
        target.hasError = ko.observable();
        target.validationMessage = ko.observable();
        function validate(newValue) {
            if (newValue == null) return false;
            target.hasError(newValue.length == 0);
            target.validationMessage(overrideMessage);
        }
        validate(target());
        target.subscribe(validate);
        return target;
    }
   
    var Category = function (id, name, recipes) {
        this.id = id;
        this.name = name;
        this.recipes = recipes;
    }
    self.recipes = ko.observableArray([]);
    self.foodCategories = ko.observableArray([]);

    self.isAddCategory = ko.observable(false);
    self.showCategoryForm = function() {
        self.isAddCategory(true);
    };

    self.newCategoryName = ko.observable(null).extend({required: "Необходимо ввести название .."});
    self.addCategory = function(formElement) {
        if (self.newCategoryName() == null) return false;
        if (self.newCategoryName().length == 0) return false;
        $.post("/Admin/CreateCategory",
            { name: self.newCategoryName() },
            function(result) {
                if (result != null) {
                    self.foodCategories.unshift(new Category(result.Id, result.Name, result.Recipes));
                    self.isAddCategory(false);
                    self.newCategoryName("");
                }
            });
    };

    self.deleteCategory = function(category) {
        $.post("/Admin/deleteCategory",
            { id: category.id },
            function(res) {
                if (res === "success") {
                    self.foodCategories.remove(category);
                }
            });
    };

    self.editCategory = function(category) {
        debugger;
        $.post("/Admin/editCategory",
            {id: id, name: name},
            function(res) {
                debugger;
            });
    };


    self.getRecipes = function () {
        $.get("/recipe/getCategories",
            function (data) {
                for (var i = 0; i < data.length; i++) {
                    self.foodCategories.push(new Category(data[i].Id, data[i].Name,data[i].Recipes));
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
                isClickRecipe: ko.observable(false),
                recipeComments: data.recipes[i].RecipeComments
            });
        }
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
                    isClickRecipe: ko.observable(false),
                    recipeComments: value.recipes[i].RecipeComments
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
                        isClickRecipe: ko.observable(false),
                        recipeComments: value.recipes[i].RecipeComments
                    });
                }
            }
        });
    });
 
    self.removeBlock = function (data) {
        debugger;
        data.isClickRecipe(false);
    }
}

ko.components.register('category',
    {
        template: { element: 'category-template' },
        viewModel: CategoryModel
    });