//var CommentViewModel = function () {
//    var self = this;
//    this.testText = 'test text';

//    self.load = function() {
//        $.ajax({
//            type: 'GET',
//            url: '/Forum/comments',
//            success: function(data) {

//            }
//        });
//    }
//}
var testModel = function (params) {
    var self = this;
    this.answerText = ko.observable();
    this.text = ko.observable('Ответить');
    this.submitBtnText = ko.observable('Отмена');
    this.isAnswer = ko.observable(false);
    this.isReadyAnswer = ko.observable(false);

    this.answer = function() {
        self.isAnswer(!self.isAnswer());
        self.isAnswer() ? self.text('') : self.text('Ответить');
    }
    this.answerText.subscribe(function(newValue) {
        if (newValue.length > 0) {
            self.isReadyAnswer(true);
        } else {
            self.isReadyAnswer(false);
        }
    });

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
