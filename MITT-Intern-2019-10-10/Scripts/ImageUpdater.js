function readURL(input, elementId) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        if (elementId.id == "profileImg") {
            reader.onload = function (e) {
                $(elementId)
                    .attr('src', e.target.result)
                    .width(200)
                    .height(200);
            };
        } else {
            reader.onload = function (e) {
                $(elementId)
                    .attr('src', e.target.result)
                    .height(150);
            };
        }

        reader.readAsDataURL(input.files[0]);
    }
}