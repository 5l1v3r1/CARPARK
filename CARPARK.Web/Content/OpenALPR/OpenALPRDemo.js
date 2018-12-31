'use strict'

/* eslint */
/* global _, $, FileReader, OpenALPRAdapter */

/**
 * init
 */
function OpenALPRAdapterDemo() {
    // Adapters
    var adapters = {
        'start': function () {
            return start()
        }
    }

    /**
     * startDemo - Adds on change event to input box.
     */
    var start = function () {
        $(document).on('change', 'input', function (input) {
            runOpenALPR(input)
        })
    }

    var runOpenALPR = function (input) {
        appendMessage('OpenALPR sorgulanirken lutfen bekleyin...')
        showImage(input)
    }

    var showImage = function (input) {
        if (!window.FileReader) {
            appendMessage('Tarayýcý FileReaderý desteklemiyor!')
            return
        }

        if (input.currentTarget.files && input.currentTarget.files[0]) {
            var reader = new FileReader()

            reader.onload = function (e) {
                var imageDataURL = e.target.result
                $('#uploadedImage').attr('src', imageDataURL)
                OpenALPRAdapter().retrievePlate(imageDataURL)
                    .then(function (response) {
                        cloudAPISuccess(response)
                    })
                    .catch(function (err) {
                        cloudAPIError(err)
                    })
            }
            reader.readAsDataURL(input.currentTarget.files[0])
        }
    }

    var cloudAPIError = function (err) {
        if (err && err.responseText) {
            appendMessage('Veri alýnamadý ' + err.responseText)
        }
        else {
            appendMessage('Istisna oluþtu', err)
        }
    }

    var cloudAPISuccess = function (response) {
        if (!response.results || response.results.length === 0) {
            appendMessage('Resimde plaka bulunamadý.')
            return
        }
        $("#messages").html("");
        for (var key in response.results) {
            var result = response.results[key]
            appendMessage('<strong>Plaka:</strong> ' + result.plate)
            appendMessage('<br/>')
            appendMessage('<strong>Bolge:</strong> Avrupa(' + result.region + ')')
            appendMessage('<br/>')
            appendMessage('<strong>Marka:</strong> ' + _.get(result, 'vehicle.make[0].name', ''))
            appendMessage('<br/>')
            appendMessage('<strong>Model:</strong> ' + _.get(result, 'vehicle.make_model[0].name', ''))
            appendMessage('<br/>')
            appendMessage('<strong>Kasa Tipi:</strong> ' + _.get(result, 'vehicle.body_type[0].name', ''))
            appendMessage('<br/>')
            appendMessage('<strong>Renk:</strong> ' + _.get(result, 'vehicle.color[0].name', ''))

            var model = {
                Plaka: result.plate,
                Bolge: result.region,
                Marka: _.get(result, 'vehicle.make[0].name', ''),
                Model: _.get(result, 'vehicle.make_model[0].name', ''),
                Kasa: _.get(result, 'vehicle.body_type[0].name', ''),
                Renk: _.get(result, 'vehicle.color[0].name', ''),
            };

            $.post('/Dashboard/AracPlakaOkumaModel', model, function (response) {});
    

        }

        var creditsRemaining = response.credits_monthly_total - response.credits_monthly_used
        // appendMessage('Credits remaining: ' + creditsRemaining)
    }

    var appendMessage = function (message) {
        console.log(message)
        $('#messages').append('<div class="message">' + message + '</div>')
    }

    // Return adapters (must be at end of adapter)
    return adapters
}

window.exports = OpenALPRAdapterDemo
// End A (Adapter)

$(document).ready(function () {
    OpenALPRAdapterDemo().start()
})
