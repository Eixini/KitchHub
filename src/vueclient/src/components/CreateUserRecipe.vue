<template>
    <div class="content">
        <h3>Добавить новый рецепт</h3>
        <div>
            <input type="text"
                   ref="enterRecipeName"
                   class="enter-recipename"
                   autocomplete="off"
                   placeholder="Введите название рецепта"
                   v-model="recipeName"/>
        </div>

        <InputAutocomplete :avaible-ingredients="avaibleIngredients"/>

        <div>
            <span> Национальность блюда </span>
            <select v-model="selectNationalKitch">
                <option v-for="nationalKitchElement in nationalKitch" v-bind:key="nationalKitchElement">
                    {{ nationalKitchElement }}
                </option>
            </select>
        </div>

        <div>
            <span> Вид блюда </span>
            <select v-model="selectDishType">
                <option v-for="dishTypeElement in dishType" v-bind:key="dishTypeElement">
                    {{ dishTypeElement }}
                </option>
            </select>
        </div>

        <div>
            <textarea v-model="recipeDescription" placeholder="Введите описание рецепта, процесс приготовления"></textarea>
        </div>

        <div>
            <button id="addRecipeButton"
                    type="button"
                    v-on:click="addRecipe()">
                Добавить рецепт
            </button>
        </div>
    </div>
</template>

<script lang="js">
    import axios from 'axios';
    import InputAutocomplete from './partial/InputAutocomplete.vue';

    export default {
        name: "createRecipe",

        mounted(){
            
            var vm = this;
            // Метод для валидации данных, запрос идет к списку ингредиентов в БД
            axios.get("https://localhost:5192/Recipe/InitialGetValidIngredients")
                 .then(function(response){
                    vm.avaibleIngredients = response.data;
                 })
                 .catch(function (error) {
                    console.log(error);
                 });
                
            // Получение списки типов блюд
            axios.get("https://localhost:5192/Recipe/GetDishType")
                 .then(function(response){
                    vm.dishType = response.data;
                 })
                 .catch(function (error) {
                    console.log(error);
                 });

            // Получение списка национальностей блюд
            axios.get("https://localhost:5192/Recipe/GetNationalKitch")
                 .then(function(response){
                    vm.nationalKitch = response.data;
                 })
                 .catch(function (error) {
                    console.log(error);
                 });
                
        },

        components: {
            InputAutocomplete,
        },

        data(){
            return {
                // Введенное имя рецепта
                recipeName: "",

                // Данные, связанные с вводом ингредиентов и их фильтрации (из списки доступных)
                avaibleIngredients: [],

                // Национальности кухни / свой рецепт
                nationalKitch: [],
                selectNationalKitch: "",

                // Вид блюда
                dishType: [],
                selectDishType: "",

                // Текст рецепта
                recipeDescription: "",
            };
        },

        methods: {
            
            // Рецепт отправляется на модерацию, после проверки добавляется в БД/удаляется/отправляется на редактирование
            addRecipe(){

            },
        }
    }
</script>