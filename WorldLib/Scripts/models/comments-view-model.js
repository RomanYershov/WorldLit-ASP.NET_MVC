


var testModel = function (params) {
    var self = this;
    this.answerText = ko.observable();
    this.text = ko.observable('Ответить');
    this.submitBtnText = ko.observable('Отмена');
    this.isAnswer = ko.observable(false);
    this.isReadyAnswer = ko.observable(false);
    this.isValidLengthMessage = ko.observable(false);
    this.answer = function() {
        this.isAnswer(!this.isAnswer());
        if (!self.isAnswer()) self.answerText('');
        self.isAnswer() ? self.text('') : self.text('Ответить');
    }

    this.answerText.subscribe(function(newValue) {
        if (newValue.length > 0) {
            self.isReadyAnswer(true);
        } else {
            self.isReadyAnswer(false);
        }
    });
    
    ko.extenders.required = function(target, message) {
    
    }
    this.answerText.extend({required: "hello"});
    //this.list = ko.observableArray([{ text: 'Roman' }, { text: 'Semen' }, {text: 'Anton'}]),
    //this.getComments = function() {
    //$.get('/forum/comments/', 
    //    {id:2},function(data) {
    //        self.list([{ text: data.Text }, { text: data.Text }, {text: data.Text}]);
    //    });
    //}
}


ko.components.register('comments', {
    template: { element: 'comments-template'},
    viewModel: testModel
        });
