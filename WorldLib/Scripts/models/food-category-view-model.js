function CategoryModel(params) {
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
        this.name = ko.observable(name);
        this.recipes = recipes;
        this.isEdit = ko.observable(false);
    }
    var Recipe = function (data) {
        var that = this;
        this.id = data.Id;
        this.name = data.Name;
        this.text = data.Description;
        this.image = data.ImageUrl;
        this.recipeComments = data.RecipeComments;
        this.isClickRecipe = ko.observable(false);
        this.newComment = ko.observable("");
        this.isEmptyComment = ko.observable(false);
        this.newComment.subscribe(function (newValue) {
            debugger;
            that.isEmptyComment(newValue.length > 0);
        });
        this.isSuccess = ko.observable(false);
        this.textInfo = ko.observable();
        this.createComment = function () {
            debugger;
            if (that.newComment().length == 0) return false;
            $.post("/Recipe/CreateComment",
                { recipeId: that.id, text: that.newComment() },
                function (data) {
                    that.newComment("");
                    that.isSuccess(true);
                    that.textInfo(data);
                });
        }

    }
    self.recipes = ko.observableArray([]);
    self.foodCategories = ko.observableArray([]);

    self.isAddCategory = ko.observable(false);
    self.showCategoryForm = function () {
        self.isAddCategory(true);
    };

    self.newCategoryName = ko.observable(null).extend({ required: "Необходимо ввести название .." });
    self.addCategory = function (formElement) {
        if (self.newCategoryName() == null) return false;
        if (self.newCategoryName().length == 0) return false;
        $.post("/Admin/CreateCategory",
            { name: self.newCategoryName() },
            function (result) {
                if (result != null) {
                    self.foodCategories.unshift(new Category(result.Id, result.Name, result.Recipes));
                    self.isAddCategory(false);
                    self.newCategoryName("");
                }
            });
    };

    self.deleteCategory = function (category) {
        $.post("/Admin/deleteCategory",
            { id: category.id },
            function (res) {
                if (res === "success") {
                    self.foodCategories.remove(category);
                }
            });
    };


    self.editCategory = function (category) {
        if (category.isEdit()) {
            $.post("/Admin/editCategory",
                { id: category.id, name: category.name },
                function (res) {
                    category.isEdit(false);
                });
        }
        category.isEdit(true);
    };
    self.editCancel = function (category) {
        category.isEdit(false);
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

    self.getRecipesByCategoryId = function (data) {
        self.recipes([]);
        for (var i = 0; i < data.recipes.length; i++) {
            self.recipes.push(new Recipe(data.recipes[i]));
        }
    }



    self.getAllRecipes = function () {
        self.recipes([]);
        $.each(self.foodCategories(), function (index, value) {
            debugger;
            for (var i = 0; i < value.recipes.length; i++) {
                self.recipes.push(new Recipe(value.recipes[i]));
            }
        });

    }
    self.getCategories();


    self.search = ko.observable();
    self.search.subscribe(function (value) {
        self.recipes([]);
        var newValue = value;
        $.each(self.foodCategories(), function (index, value) {
            for (var i = 0; i < value.recipes.length; i++) {
                var nameToUpperCase = value.recipes[i].Name.toUpperCase();
                if (nameToUpperCase.indexOf(newValue.toUpperCase()) != -1) {
                    self.recipes.push(new Recipe(value.recipes[i]));
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