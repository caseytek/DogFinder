

function ToggleMixedBreedSelection() {
    $('#breed-selector').addClass('hidden');
    $('#mixed-breed-selector').removeClass('hidden');
    let breeds = $('#dog_breed_chosen li');
    for (let breed in breeds) {
        if ($(breed).html() == 'Mixed Breed') {
            $(breed).click();
        }
    }
};

function ToggleBreedSelection() {
    $('#breed-selector').removeClass('hidden');
    $('#mixed-breed-selector').addClass('hidden');
    $('a.search-choice-close').click();
}

$(function () {
    $('#dog-breed').chosen({
        placeholder_text_single: "Select...",
        no_results_text: "Oops, nothing found!"
    });
});
$(function () {
    $('#dog-breeds').chosen({
        placeholder_text_single: "Select...",
        no_results_text: "Oops, nothing found!",
        width: "100%"
    });
});
$(function () {
    $('#dog-color').chosen({
        placeholder_text_single: "Select...",
        no_results_text: "Oops, nothing found!"
    });
});