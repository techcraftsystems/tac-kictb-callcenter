var linearStepper = document.querySelector('#linearStepper');
var linearStepperInstace = new MStepper(linearStepper, {
    firstActive: 0,
    showFeedbackPreloader: true,
    autoFormCreation: true,
    stepTitleNavigation: true,
    feedbackPreloader: '<div class="spinner-layer spinner-blue-only">...</div>'
});

linearStepperInstace.resetStepper();

function validationFunction(stepperForm, activeStepContent) {
    someValidationPlugin(stepperForm);
    return true;
}

function defaultValidationFunction(stepperForm, activeStepContent) {
    var inputs = activeStepContent.querySelectorAll('input, textarea, select');
    for (var i = 0; i < inputs.length; i++) {
        if (!inputs[i].checkValidity()) return false;
    }
    return true;
}

jq('.btn-reset').on('click', function () {
    linearStepperInstace.openStep(0);
})

jq('input.telephone').formatter({
    'pattern': '+254 {{999}} {{99}} {{9999}}',
    'persistent': true
});

jq('#Contact_Age').formatter({
    'pattern': '{{99}}',
    'persistent': true
});

$('textarea.counter').characterCounter();

$('input.community-unit').autocomplete({
    data: units
});

$('input.facility').autocomplete({
    data: facilities
});

$('input.others').autocomplete({
    data: conditions
});


jq(function() {
    jq('#Contact_Age').on('blur', function(){
        var age = jq(this).val();
        if (eval(age) > 12){
            jq("#Screen_Playfullness").prop('disabled', true);
            jq('#Screen_Playfullness').prop('checked', false);
        }
        else{
            jq("#Screen_Playfullness").prop('disabled', false);
        }
    });

    jq('#Screen_Outcome_Id').change(function(){
        var index = eval(jq(this).val()); 
        if (index == 1) {
            jq('div.screen.tb').show(300);
            jq('div.screen.other').hide();
            jq('div.screen.other input').val('');
        }
        else if (index == 2) {
            jq('div.screen.other').show(300);
            jq('div.screen.tb').hide();
            jq('div.screen.tb input').val('');
        }
        else {
            jq('div.screen.tb').hide();
            jq('div.screen.other').hide();
            jq('div.screen.tb input').val('');
            jq('div.screen.other input').val('');
        }
    });

    jq('#Record_Caller_Id').change(function(){
        var index = eval(jq(this).val());
        if (index == 1) {
            jq('div.caller-dtls').hide(300);
            jq('div.caller-dtls input').val('');
        }
        else{
            jq('div.caller-dtls').show(300);
        }
    });

    jq("#Record_Referral_Id").change(function(){
        jq.ajax({
            dataType: "json",
            url: '/Contacts/GetRefereesAutocomplete',
            data: {
                "catg": jq(this).val()
            },
            success: function(refs) {
                jq('input.referee').autocomplete({
                    data: refs
                });
            },
            error: function(xhr, ajaxOptions, thrownError) {
                console.log(xhr.status);
                console.log(thrownError);
            },complete: function() {
                jq('body').removeClass('loaded');
                jq('input.referee').val('');
            }
        });
    });

});