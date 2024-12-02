function loadDataTable() {
    $("#category-table").DataTable({
        ajax: { url: '/category/get' },
        columns: [
            { data: 'name', width: '45%' },
            { data: 'displayOrder', width: '45%' },
            {
                data: 'id',
                render: function (data) {
                    return `<div class="w-100 btn-group" role="group">
                        <a href="/category/update?id=${data}" class="btn btn-primary mx-2">
                            <i class="bi bi-pencil-square"></i> Edit
                        </a>
                        <form method="post" action="/category/delete" onsubmit="return confirm('Do you really want to delete category @obj.Name');">
                            <input type="hidden" name="Id" value="${data}" />
                            <button type="submit" class="btn btn-danger">
                                <i class="bi-trash"></i>Delete
                            </button>
                        </form>
                    </div>`;
                },
                width: '10%'
            }
        ]
       }
    );
}

$(document).ready(function () {
    loadDataTable();
});