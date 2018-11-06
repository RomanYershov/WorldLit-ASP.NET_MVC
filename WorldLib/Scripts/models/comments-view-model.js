


var answerModel = function (params) {
    var self = this;
    var par = params;
 
    self.answerText = ko.observable();
    self.text = ko.observable('Ответить');
    self.submitBtnText = ko.observable('Отмена');
    self.isAnswer = ko.observable(false);
    self.isReadyAnswer = ko.observable(false);
    self.isValidLengthMessage = ko.observable(false);
    self.answer = function() {
        self.isAnswer(!self.isAnswer());
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
    self.answerText.extend({required: "hello"});

}


ko.components.register('comments', {
    template: { element: 'comments-template'},
    viewModel: answerModel
        });
