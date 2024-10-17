$(document).ready(function () {
    $('#dataTable').DataTable({
        searching: true,
        ordering: true,
        paging: true,
        lengthMenu: [25],
        responsive: true,
        colReorder: true,
        dom: 'B<"clear">frtip',
        buttons: {
            name: 'primary',
            buttons: [{ extend: 'excel', text: 'Save as Excel' }, 'print']
        }
    });
});