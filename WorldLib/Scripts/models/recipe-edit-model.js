
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


        self.selectedFile = ko.observable();
        self.fileUploadChange = function(data, evt) {
            self.selectedFile(null);
            self.selectedFile(evt.target.files[0]);
        }

        
       
        self.createRecipe = function (params) {
            var dstep = descriptionSteps();
            var file = self.selectedFile();
            var formData = new FormData();
            formData.append("file", file);
            debugger;
            $.ajax({
                type: "POST",
                url: "/Admin/UploadFile",
                data: formData,
                cache: false,
                dataType: 'json',
                processData: false, // Не обрабатываем файлы (Don't process the files)
                contentType: false, // Так jQuery скажет серверу что это строковой запрос
                success: function (result) {
                   
                }
            });
            var arr = [];
            $.each(dstep, function() {
                arr.push(this.text());
            });
            $.post("/Admin/createRecipe",
                {
                    newRecipeName:newRecipeName,
                    categoryId: categoryId,
                    descriptionSteps: arr,
                    image: file.name
                },
                function (result) {
                    debugger;
                    self.recipes.unshift({
                        id: result.Id,
                        name: result.Name,
                        text: result.Description,
                        image: result.ImageUrl,
                        categoryName: result.CategoryName
                    });
                    self.isVisibleForm(false);
                    newRecipeName(null);
                    descriptionSteps([]);
                    image(null);
                    self.selectedFile(null);
                });
        } 
    }
    
    self.isVisibleForm = ko.observable(false);
    self.showFormRecipe = function () {
        self.isVisibleForm(!self.isVisibleForm());
    }

    self.removeRecipe = function(recipe) {
        $.post("/Admin/RemoveRecipe", { id: recipe.id },
            function(res) {
                if (res === "success") {
                    self.recipes.remove(recipe);
                }
            });
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