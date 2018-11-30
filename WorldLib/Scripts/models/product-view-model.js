
function ProductModel(params) {
    var self = this;

    self.proceses = {
        none: 0,
        create: 1,
        update: 2,
        remove: 3
    }

    ko.extenders.changeText = function (target, messages) {
        target.text = ko.observable(messages[0]);
        target.subscribe(function (newValue) {
            newValue ? target.text(messages[1]) : target.text(messages[0]);
        });
    }
    ko.extenders.weightInpHolder = function (target, options) {
        target.placeholder = ko.observable(options[1]);
        target.subscribe(function (newValue) {
            target.placeholder(newValue === "number" ? options[0] : options[1]);
        });
    }

    ko.extenders.totalSumText = function (target, text) {
        target.showText = ko.observable(text + target());
        target.subscribe(function (newValue) {
            target.showText(text + newValue);
        });
    }
    self.isCreateProductClick = ko.observable(false).extend({ changeText: ["Создать продукт", "Создать"] });
    self.newProductName = ko.observable("");
    self.products = ko.observableArray([]);



    var Ingridient = function (parent) {
        //parent.calcSum();
        var that = this;
        that.Id = '';
        that.ParentId = parent.id;
        that.Name = ko.observable('');
        that.Cost = ko.observable().extend({ required: "" });
        that.Weight = ko.observable().extend({ required: "" });
        that.ProductId = ko.observable(parent.id);
        that.ProcessFlag = ko.observable(self.proceses.create);
        that.InputType = ko.observable("text").extend({ weightInpHolder: ["кол-во(шт)", "вес(гр)"] });
        that.InputType.subscribe(function () {
            that.Weight('');
        });
        parent.cost(this.Cost() == null ? 0 : this.Cost());

        //that.Cost.subscribe(function (newVal) {
        //    parent.calcSum();
        //});
    }
    var Product = function (name) {
        var that = this;
        that.name = name;
        that.cost = ko.observable(0).extend({ totalSumText: "Стоимость: " });
        that.weight = 0;
        that.description = ko.observable("");
        that.id = 0;
        that.lastChangeDate = ko.observable(new Date().toLocaleDateString() + '/' + new Date().toLocaleTimeString());
        that.total = ko.observable().extend({ required: "Введите кол-во изделий(порций)" });
        that.isNewOrUpdatedProduct = ko.observable(true);
        that.isEdit = ko.observable(true); 
       // self.isValidProduct = ko.observable(false);
        that.isDescription = ko.observable(false).extend({ changeText: ["заметки", "скрыть"] });
        that.ingridients = ko.observableArray([new Ingridient(this)]);
    }
    ko.extenders.required = function (target, overrideMessage) {
        target.hasError = ko.observable();
        target.validationMessage = ko.observable();
        function validate(newValue) {
            if (isNaN(newValue) || newValue == 0) target(null);
            target.hasError(target() ? false : true);
            target.validationMessage(target() ? "" : overrideMessage);
        }
        validate(target());
        target.subscribe(validate);
        return target;
    }

    var WeightType = function (name, value) {
        this.typeName = name;
        this.typeValue = value;
    }
    self.weightOptions = ko.observableArray([
        new WeightType("гр", "text"),
        new WeightType("ед", "number")
    ]);

    self.validationProduct = function (product) {
        var count = 0;
        debugger;
        if (product.total.hasError())++count;
        for (var i = 0; i < product.ingridients().length; i++) {
            if (product.ingridients()[i].ProcessFlag() == self.proceses.remove)
                continue;
            if (product.ingridients()[i].Cost.hasError())++count;
            if (product.ingridients()[i].Weight.hasError())++count;
        }
        return count;
    }

    
    self.calcSum = function (product) {
        debugger;
        if (self.validationProduct(product) > 0) {
          //  product.isValidProduct(false);
            return false;
        };
       // product.isValidProduct(true);
        var result = 0;
        $.each(product.ingridients(), function () {
            debugger;
            if (this.ProcessFlag() !== self.proceses.remove) {
                if (this.InputType() === "text") {
                    var nWeightProc = parseInt(this.Weight()) / 10;
                    var nCostProc = parseInt(this.Cost()) / 100;
                    result += nWeightProc * nCostProc;
                }
                if (this.InputType() === "number") {
                    debugger;
                    var nOneCost = parseInt(this.Cost()) / 10;
                    result += nOneCost  * parseInt(this.Weight());
                }
            }
        });
        result /= product.total();
        product.cost(Math.round(result));
        return true;
    }
    self.showDescription = function (product) {
        product.isDescription(!product.isDescription());
    }

    //ko.extenders.dateTimeFormat = function (target, date) {
    //    target.showDate = ko.computed({
    //        read: function () {
    //            target.subscribe(function (newValue) {
    //                return newValue.toLocaleDateString() + '/' + newValue.toLocaleTimeString();
    //            });
    //            return date.toLocaleDateString() + '/' + date.toLocaleTimeString();
    //        },
    //        write: function (value) {
    //            target(value);
    //        },
    //        owner: this
    //    });
    //    //target.subscribe(function (newValue) {
    //    //    debugger;
    //    //    target.showDate = ko.computed(function () {
    //    //        return newValue.toLocaleDateString() + '/' + newValue.toLocaleTimeString();
    //    //    });
    //    //});
    //}

    self.getProducts = function () {
        $.get('/product/GetProducts',
            function (data) {
                for (var i = 0; i < data.length; i++) {
                    self.products.push({
                        id: data[i].Product.Id,
                        name: data[i].Product.Name,
                        cost: ko.observable(data[i].Product.Cost).extend({ totalSumText: "Стоимость: " }),
                        weight: data[i].Product.Weight,
                        total: ko.observable(data[i].Product.Total).extend({ required: "Введите кол-во изделий(порций)" }),
                        description: ko.observable(data[i].Product.Description),
                        lastChangeDate: ko.observable(new Date(parseInt(data[i].Product.LastChangeDate.match(/[0-9]+/))).toLocaleDateString()
                            + '/' + new Date(parseInt(data[i].Product.LastChangeDate.match(/[0-9]+/))).toLocaleTimeString()),
                        isNewOrUpdatedProduct: ko.observable(false),
                        isEdit: ko.observable(false),
                        isDescription: ko.observable(false).extend({ changeText: ["заметки", "скрыть"] }),
                        ingridients: ko.observableArray(self.setBindings(data[i].Ingridients))
                    });
                }
            });
    };
    self.getProducts();
    self.setBindings = function (ingridients) {
        $.each(ingridients, function () {
            var that = this;
            that.Name = ko.observable(this.Name);
            that.Cost = ko.observable(this.Cost).extend({ required: "" });
            that.Weight = ko.observable(this.Weight).extend({ required: "" });
            that.ProcessFlag = ko.observable(this.ProcessFlag);
            that.InputType = ko.observable(this.InputType).extend({ weightInpHolder: ["кол-во(шт)", "вес(гр)"] });
            that.InputType.subscribe(function () {
                that.Weight('');
            });
        });
        return ingridients;
    }

    self.removeProduct = function (product) {
        //переделать
        $.post("/product/RemoveProduct",
            { id: product.id },
            function (data) {
                self.products.remove(product);
                swal({
                    title: "Продукт удален",
                    text: product.name,
                    icon: "success",
                    buttons: false,
                    timer: 2000
                });
            });
    }

    self.editProduct = function (product) {
        product.isEdit(true);
        product.isNewOrUpdatedProduct(true);
        $.each(product.ingridients(), function () {
            if (this.ProcessFlag() !== self.proceses.remove)
                this.ProcessFlag(self.proceses.update);
        });
    }

    self.createProductForm = function () {
        self.isCreateProductClick(true);
        if (self.isCreateProductClick() && self.newProductName().length > 0) {
            createProduct();
            self.isCreateProductClick(false);
            self.newProductName("");
        }
    }


    function createProduct() {
        var newName = self.newProductName();
        self.products.unshift(new Product(newName));
    }



    self.addIngridient = function (product) {
        product.ingridients.push(new Ingridient(product));
        product.isNewOrUpdatedProduct(true);
    }



    self.removeIngridient = function (ingridient) {
        $.each(self.products(),
            function () {
                var isMyProduct = this.ingridients().includes(ingridient);
                if (isMyProduct) {
                    ingridient.ProcessFlag(self.proceses.remove);
                    this.isNewOrUpdatedProduct(true);
                }
                return !isMyProduct;
            }); // writen each js
    }

    self.saveProduct = function (product) {
        debugger;
        $.post('/product/saveProduct',
            self.setData(product),
            function (data) { // simple response realisovat
                debugger;
                product.isNewOrUpdatedProduct(false);
                product.isEdit(false);
                product.id = data.Id;
                //product.total = ko.observable(data.Total).extend({ required: "Введите кол-во изделий(порций)" });
                product.lastChangeDate(new Date(parseInt(data.LastChangeDate.match(/[0-9]+/))).toLocaleDateString()
                    + '/' + new Date(parseInt(data.LastChangeDate.match(/[0-9]+/))).toLocaleTimeString());
                product.ingridients(self.setBindings(data.Ingridients));
            });
    }

    self.setData = function (product) {
        return {
            name: product.name,
            cost: product.cost,
            description: product.description,
            weight: product.weight,
            id: product.id,
            total: product.total,
            isNewOrUpdatedProduct: product.isNewOrUpdatedProduct(),
            ingridients: product.ingridients()
        }
    }

    self.save = function (product) {
        self.lastSavedJson(JSON.stringify(ko.toJS(product), null, 2));
    };

    self.lastSavedJson = ko.observable("");
}


ko.components.register('products', {
    template: { element: 'product-template' },
    viewModel: ProductModel
});


