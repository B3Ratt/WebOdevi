$(document).ready(function () {
    $.ajax({
        url: '/api/Berber',
        type: 'GET',
        success: function (data) {
            var tableBody = $('#berberler-tbody');
            tableBody.empty();  // Önceden eklenmiþ satýrlarý temizleyin
            $.each(data, function (index, berber) {
                var row = '<tr>' +
                    '<td>' + berber.ad + '</td>' +
                    '<td>' + berber.soyad + '</td>' +
                    '<td>' + berber.uzmanlik + '</td>' +
                    '<td>' + (berber.musaitlik ? 'Evet' : 'Hayýr') + '</td>' +
                    '</tr>';
                tableBody.append(row);
            });
        },
        error: function () {
            alert("Berber verileri yüklenirken bir hata oluþtu.");
        }
    });
});
