
function ProductModel(params) {


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
    var Ingridient = function (parent) {
        //parent.calcSum();
        var that = this;
        debugger;
        that.Id = '';
        that.ParentId = parent.id;
        that.Name = ko.observable('');
        that.Cost = ko.observable(0);
        that.Weight = ko.observable('');
        that.ProductId = ko.observable(0);
        that.ProcessFlag = ko.observable(self.proceses.create);
        
        parent.cost(this.Cost());
        that.Cost.subscribe(function (newVal) {
            parent.calcSum();
        });
    }
    var Product = function (name) {
        var that = this;
        that.name = name;
        that.cost = ko.observable(0);
        that.weight = 0;
        that.description = '';
        that.id = 0;
        that.isNewOrUpdatedProduct = ko.observable(true);
        that.isEdit = ko.observable(true);
        that.ingridients = ko.observableArray([new Ingridient(this)]);

        that.calcSum = function () {
            var res = 0;
            $.each(that.ingridients(), function () {
                if (this.ProcessFlag() !== self.proceses.remove)
                    res += parseInt(this.Cost());
                that.cost(res);
            });
        }
    }

    self.getProducts = function () {
        $.get('/product/GetProducts',
            function (data) {
                for (var i = 0; i < data.length; i++) {
                    self.products.push({
                        id: data[i].Product.Id,
                        name: data[i].Product.Name,
                        cost: ko.observable(data[i].Product.Cost),
                        weight: data[i].Product.Weight,
                        description: data[i].Product.Description,
                        isNewOrUpdatedProduct: ko.observable(false),
                        isEdit: ko.observable(false),
                        ingridients: ko.observableArray(self.setBindings(data[i].Ingridients))
                    });
                }
            });
    };
    self.getProducts();
    self.setBindings = function (ingridients) {
        $.each(ingridients, function () {
            this.Name = ko.observable(this.Name);
            this.Cost = ko.observable(this.Cost);
            this.Weight = ko.observable(this.Weight);
            this.ProcessFlag = ko.observable(this.ProcessFlag);
        });
        return ingridients;
    }

    self.removeProduct = function (product) {
        self.products.remove(product); //переделать
        $.post("/product/RemoveProduct",
            { id: product.id },
            function (data) {

            });
    }

    self.editProduct = function (product) {
        debugger;
        product.isEdit(true);
        product.isNewOrUpdatedProduct(true);
        $.each(product.ingridients(), function () {
            if (this.ProcessFlag() !== self.proceses.remove)
                this.ProcessFlag(self.proceses.update);
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
        self.products.push(new Product(newName));
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
            self.getData(product),
            function (data) {
                product.isNewOrUpdatedProduct(false);
                product.isEdit(false);
                product.id = data.Id;
                product.ingridients(self.setBindings(data.Ingridients));
                debugger;
            });
    }

    self.getData = function (product) {
      
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


