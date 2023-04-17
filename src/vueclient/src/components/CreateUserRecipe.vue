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

        <InputAutocomplete :avaible-ingredients="avaibleIngredients"
                           @ingredientsAdded="ingredientAdd"
                           @ingredientRemoved="ingredientRemove"/>

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
    import { store } from '@/store';
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
                selectIngredients: [],

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
           //getIngredientsFromTags(ing){       
             //   let ingredientsArray = new Array();
               // ing.forEach(element => {
                 //   ingredientsArray.push(element);
               // });
                //return ingredientsArray;
            //},
            
            // Рецепт отправляется на модерацию, после проверки добавляется в БД/удаляется/отправляется на редактирование
            addRecipe(){
                var vm = this;
                console.log('Select DishType: ' + vm.selectDishType);
                axios.post('https://localhost:5192/Recipe/CreateRecipe', {
                    name: vm.recipeName,
                    description: vm.recipeDescription,
                    Type: vm.selectDishType,
                    nationalkitch: vm.selectNationalKitch,
                    ingredients: vm.selectIngredients, // получать ингредиенты из списка (РЕАЛИЗОВАТЬ!)
                    WhoAdded: vm.currentUser.nickname,
                }, )
                .then(response => {
                    alert(response.data);
                })
                .catch(function (error) {
                    console.log(error.response.data);
                });
            },

            ingredientAdd(ing){
                //console.log('Ing add:' + ing);
                let vm = this;
                vm.selectIngredients.push(ing);
                
                // Отладка
                store.getters.getIngredients.forEach(el => {
                    console.log(el);
                });
            },

            ingredientRemove(index){
                let vm = this;
                //vm.selectIngredients.splice(index,1);
                //delete vm.selectIngredients[index];

                // Отладка
                console.log('======== Ingredients array ========');
                store.getters.getIngredients.forEach(el => {
                    console.log(el);
                });
            }
        },
        
        computed: {
            currentUser() {
                return this.$store.state.auth.user;
            }
        },
    }
</script>