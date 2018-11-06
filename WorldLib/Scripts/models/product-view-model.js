﻿
function ProductModel(products) {
    var self = this;
    self.isCreateProductClick = ko.observable(false);
    self.btnProductVal = ko.observable("Создать продукт");
    self.newProductName = ko.observable("");
    self.products = ko.observableArray([]);
    self.proceses = {
        none: 0,
        create: 1,
        update: 2,
        remove: 3
    }

    self.getProducts = function () {
        $.get('/product/GetProducts',
            function (data) {
                for (var i = 0; i < data.length; i++) {
                    self.products.push({
                        id: data[i].Product.Id,
                        name: data[i].Product.Name,
                        cost: data[i].Product.Cost,
                        weight: data[i].Product.Weight,
                        description: data[i].Product.Description,
                        isNewOrUpdatedProduct: ko.observable(false),
                        ingridients: ko.observableArray(self.setProcessFlag(data[i].Ingridients))
                    });
                }
            });
    };
    self.getProducts();
    self.setProcessFlag = function (ingridients) {
        $.each(ingridients, function () { this.ProcessFlag = ko.observable(this.ProcessFlag) });
        return ingridients;
    }

    self.removeProduct = function (product) {
        self.products.remove(product); //переделать
        $.post("/product/RemoveProduct",
            { id: product.id },
            function (data) {

            });
    }



    self.createProductForm = function () {
        self.isCreateProductClick(true);
        self.btnProductVal("Создать");
        if (self.isCreateProductClick() && self.newProductName().length > 0) {
            createProduct();
            self.isCreateProductClick(false);
            self.newProductName("");
            self.btnProductVal("Создать продукт");
        }
    }

    function createProduct() {
        var newName = self.newProductName();
        self.products.push({
            name: newName,
            cost: 0,
            weight: 0,
            description: '',
            id: 0,
            isNewOrUpdatedProduct: ko.observable(true),
            ingridients: ko.observableArray([{ Name: '', Weight: '', Cost: '', ProcessFlag: ko.observable(0) }])
        });
    }

    self.addIngridient = function (product) {
        product.ingridients.push({
            ProductId: product.id,
            Name: '',
            Weight: '',
            Cost: '',
            ProcessFlag: ko.observable(self.proceses.create)
        });
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
        $.post('/product/saveProduct',
            self.getData(product),
            function (data) {
                debugger;
            });
    }

    self.getData = function (product) {
        product.isNewOrUpdatedProduct(false);
        return {
            name: product.name,
            cost: product.cost,
            description: product.description,
            weight: product.weight,
            id: product.id,
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


