// Добавление "тега" в поле тегов из введеного ингредиента в строке ввода
function addTag(newTag, tagsDivId) {
    // Получение элемента "поле тегов"
    var id = document.querySelector("#current_tags");
    // Добавление внутрь "поля тегов" нового "тега"
    id.innerHTML += "<button type=\"button\"   value=\""
        + newTag.value + "\" onclick=\"removeTag(this,'"
        + tagsDivId + "')\">"
        + newTag.value + "</button>";

    // Удаление содержимого строки ввода
    newTag.value = "";
}
// Удаление "тега" посредством клика на сам "тег"
function removeTag(button, tagsDivId) {
    var id = document.querySelector("#current_tags");
    id.removeChild(button);
}

// Сбор введенных ингредиентов в массив и передача его в backend слой
function sendIngredients() {
    var tagField = document.querySelector("#current_tags");

    // Массив для хранения ингредиентов из "поля тегов"
    let ingredientsArray = new Array();
    let ingredientsCount = tagField.children.length;

    // Проверка на количество введенных ингредиентов
    if (ingredientsCount === 0) {
        // Сообщение о том, что нет введенных ингредиентов
        alert("Нет введенных ингредиентов");
    } else {
        // Получение ингредиентов из формы


        for (let i = 0; i < tagField.childNodes.length; i++) {
            ingredientsArray.push(tagField.childNodes[i].innerText);
        }
        let selectIng = ingredientsArray.join(',');
        alert("JS: Количество введенных ингредиентов = "
            + ingredientsArray.length);

        // Передача массива ингредиентов в BackEnd слой
        $.ajax({
            url: `/RecipeSearch/SearchRecipe?ingredients=${selectIng}`,
            type: 'GET',
            cache: false,
            processData: false,
            success: function (ingredients) {
                if (ingredients) {
                    //alert(ingredients.three);
                } else {
                    alert("Сообщение не доставлено!");
                }
            },
            error: function (xhr, status, err) {
                alert(xhr + status + ": " + err);
            }
            });
    }
}