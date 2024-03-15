const formErrorMessage = (element, validationResult) => {

    let spanElement = document.querySelector(`[data-valmsg-for="${element.name}"]`)

    if (validationResult) {
        element.classList.remove('input-validation-error')
        spanElement.classList.remove('field-validation-error')
        spanElement.classList.add('field-validation-valid')
        spanElement.innerHTML = ""


    } else {
        element.classList.add('input-validation-error')
        spanElement.classList.add('field-validation-error')
        spanElement.classList.remove('field-validation-valid')
        spanElement.innerHTML = element.dataset.valRequired
    }
}



const compareValidator = (value, compareValue) => {
    if (value === compareValue) {
        return true
    }
    else {
        return false
    }
}


const textValidator = (element, minlength = 2) => {
   
    if (element.value.length >= minlength) {
       
        formErrorMessage(element, true)
    }
    else {  
       
        formErrorMessage(element, false)
    }
}

const emailValidator = (element) => {
    const emailRegex = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/
    formErrorMessage(element, emailRegex.test(element.value))
}

const passwordValidator = (element) => {
    if (element.dataset.valEqualToOther !== undefined) {
       
        formErrorMessage(element, compareValidator(element.value, document.getElementsByName(element.dataset.valEqualToOther.replace('*', 'Form')[0].value)))
    }
    else {
        const passwordRegex = /^(?=.*[0-9])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]{7,15}$/
        formErrorMessage(element, passwordRegex.test(element.value))
    }
}

const checkBoxValidator = (element) => {

    if (element.checked) {
        formErrorMessage(element, true)
    }
    else {
        formErrorMessage(element, false)
    }
}
let forms = document.querySelectorAll('form')
let inputs = forms[0].querySelectorAll('input')


inputs.forEach(input => {
    if (input.dataset.val === 'true') {

        if (input.type === 'checkbox') {
            input.addEventListener('change', (e) => {
                checkBoxValidator(e.target)
            })
        }
        else {
            input.addEventListener('keyup', (e) => {
                switch (e.target.type) {
                    case "text":
                        textValidator(e.target)
                        break
                    case "email":
                        emailValidator(e.target)
                        break
                    case "password":
                        passwordValidator(e.target)

                }
            })
        }
    }
})



//const formErrorMessage = (element, validationResult) => {
//    let spanElement = document.querySelector(`[data-valmsg-for="${element.name}"]`);

//    if (validationResult) {
//        element.classList.remove('input-validation-error');
//        spanElement.classList.remove('field-validation-error');
//        spanElement.classList.add('field-validation-valid');
//        spanElement.innerHTML = "";
//    } else {
//        element.classList.add('input-validation-error');
//        spanElement.classList.add('field-validation-error');
//        spanElement.classList.remove('field-validation-valid');
//        spanElement.innerHTML = element.dataset.valRequired;
//    }
//};

//const checkBoxValidator = (element) => {
//    if (element.checked) {
//        formErrorMessage(element, true);
//    } else {
//        formErrorMessage(element, false);
//    }
//};


//let forms = document.querySelectorAll('form');
//let inputs = forms[0].querySelectorAll('input');

//inputs.forEach(input => {
//    if (input.dataset.val === 'true') {
//        if (input.type === 'checkbox') {
//            input.addEventListener('change', (e) => {
//                checkBoxValidator(e.target);
//            });
//        }
//    }
//});
