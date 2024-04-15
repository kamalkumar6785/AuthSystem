// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function openAddItemModal() {
    $('#addItemModal').modal('show');

    $('#itemName').val('');
    $('#itemVal').val('');


}

function editItem(button) {


    var row = $(button).closest('tr');
    var itemName = row.find('td:first-child').text();
    var itemValue = row.find('td:nth-child(2)').text();
    itemValue = itemValue.trim();

    $('#editItemName').val(itemName);
    $('#editItemValue').val(itemValue);
    var itemId = $(button).data('item-id');
    $('#editItemId').val(itemId);


    $('#editItemModal .modal-title').text('Edit Item');
    $('#editItemForm').attr('asp-action', 'Edit');

    $('#editItemModal').modal('show');
}

function closeEditModal() {
    $('#editItemModal').modal('hide');
}

function closeAddItem() {
    $('#addItemModal').modal('hide');

}

function deleteItem(button) {

    $(button).closest('tr').remove();
}
