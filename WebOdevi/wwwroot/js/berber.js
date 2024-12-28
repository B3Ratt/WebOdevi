$(document).ready(function () {
    $.ajax({
        url: '/api/Berber',
        type: 'GET',
        success: function (data) {
            var tableBody = $('#berberler-tbody');
            tableBody.empty();  // �nceden eklenmi� sat�rlar� temizleyin
            $.each(data, function (index, berber) {
                var row = '<tr>' +
                    '<td>' + berber.ad + '</td>' +
                    '<td>' + berber.soyad + '</td>' +
                    '<td>' + berber.uzmanlik + '</td>' +
                    '<td>' + (berber.musaitlik ? 'Evet' : 'Hay�r') + '</td>' +
                    '</tr>';
                tableBody.append(row);
            });
        },
        error: function () {
            alert("Berber verileri y�klenirken bir hata olu�tu.");
        }
    });
});
