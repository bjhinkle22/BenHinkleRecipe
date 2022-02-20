// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
    $(document).ready(function() {
        $("#searchbox").on("keyup", function () {
            var value = $(this).val().toLowerCase();
            $('div[data-role="recipe"]').filter(function () {
                $(this).toggle($(this).find('h5').text().toLowerCase().indexOf(value) > -1)
            });
        });
});

    function confirmDelete(uniqueId, isDeleteClicked){
    var deleteSpan = 'deleteSpan_' + uniqueId;
    var confirmDeleteSpan = 'confirmDeleteSpan_' + uniqueId;

    if(isDeleteClicked){
        $('#' + deleteSpan).hide();
    $('#' + confirmDeleteSpan).show();
    }
    else{
        $('#' + deleteSpan).show();
    $('#' + confirmDeleteSpan).hide();
    }
}