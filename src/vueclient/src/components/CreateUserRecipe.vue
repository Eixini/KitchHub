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

        <div class="national-select">
            <span> Национальность блюда </span>
            <select v-model="selectNationalKitch">
                <option v-for="nationalKitchElement in nationalKitch" v-bind:key="nationalKitchElement">
                    {{ nationalKitchElement }}
                </option>
            </select>
        </div>

        <div class="dishtype-select">
            <span> Вид блюда </span>
            <select v-model="selectDishType">
                <option v-for="dishTypeElement in dishType" v-bind:key="dishTypeElement">
                    {{ dishTypeElement }}
                </option>
            </select>
        </div>

        <div class="recipe-description">
            <textarea v-model="recipeDescription" placeholder="Введите описание рецепта, процесс приготовления"></textarea>
        </div>

        <div>
            <button id="addRecipeButton"
                    class="btn-send"
                    type="button"
                    v-on:click="addRecipe()">
                Добавить рецепт
            </button>
        </div>
    </div>
</template>

<script lang="js">
    import AxiosInstance from '@/api_instance';
    import { store } from '@/store';
    import InputAutocomplete from './partial/InputAutocomplete.vue';

    export default {
        name: "createRecipe",

        mounted(){
            
            var vm = this;
            // Метод для валидации данных, запрос идет к списку ингредиентов в БД
            AxiosInstance.get("Recipe/InitialGetValidIngredients")
                 .then(function(response){
                    vm.avaibleIngredients = response.data;
                 })
                 .catch(function (error) {
                    console.log(error);
                 });
                
            // Получение списки типов блюд
            AxiosInstance.get("Recipe/GetDishType")
                 .then(function(response){
                    vm.dishType = response.data;
                 })
                 .catch(function (error) {
                    console.log(error);
                 });

            // Получение списка национальностей блюд
            AxiosInstance.get("Recipe/GetNationalKitch")
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
                AxiosInstance.post('Recipe/CreateRecipe', {
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
                console.log('======== Ingredients array (after added) ========');
                store.getters.getIngredients.forEach(el => {
                    console.log(el);
                });
            },

            ingredientRemove(index){
                let vm = this;
                //vm.selectIngredients.splice(index,1);
                //delete vm.selectIngredients[index];

                // Отладка
                console.log('======== Ingredients array (after removed) ========');
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

<style scoped>

.content {
    margin: 0em;
    padding: 0em;
    display: block;
    align-items: center;
}

.enter-recipename {
    font-family: 'Ubuntu';
    font-size: 1em;
    color: #263238;

    width: max-content;
    align-items: center;
    margin: 1em;
}

.national-select {

}

.dishtype-select {

}

.recipe-description {

}

textarea {

    font-family: 'Ubuntu';
    font-size: 1em;

    width: 100%;
    resize: none;
    padding: 0.5em;
    border-radius: 0.3em;
    border:0;
    height: 150px;
}

.btn-send {
    
    font-family: 'Ubuntu';
    font-size: 1em;

    outline: none;
    border: 0.3em solid transparent; 
    border-radius: 0.3em;
    box-sizing: border-box;
    color: #263238;
    background-color: #35BFA2;
    cursor: pointer;
    position: fixed;
    bottom: 1em;
    z-index: 500;
    align-items: center;

}

</style>