<template>

    <div class="content">
        <InputAutocomplete :avaible-ingredients="avaibleIngredients"/>

        <button id="sendIngredientsButton"
                type="button"
                v-on:click="sendIngredients"
                class="btn-send">Запросить
        </button>
    </div>

</template>

<script lang="js">

import { store } from '@/store';
import InputAutocomplete from './partial/InputAutocomplete.vue';
import AxiosInstance from '@/api_instance';

export default {

    components: {
        InputAutocomplete,
    },

    mounted(){

        // Очистка списка ингредиентов из хранилища
        store.commit('clearIngredientList');

        var vm = this;
            // Метод для валидации данных, запрос идет к списку ингредиентов в БД
            AxiosInstance.get("Recipe/InitialGetValidIngredients")
                 .then(function(response){
                    vm.avaibleIngredients = response.data;
                    console.log(vm.avaibleIngredients);
                 })
                 .catch(function (error) {
                    console.log(error);
                 });
    },
    
    name: 'EnteringIngredients',

    data() {
        return {
            avaibleIngredients: [],
            resultRecipe: {},
        };
    },
    methods: {
 
        // Метод для отправки введенных ингредиентов на сервер для поиска рецептов
        sendIngredients(){
            let ingredientsArray = new Array();

            var vm = this;

            store.getters.getIngredients.forEach(element => {
                ingredientsArray.push(element);
                console.log(element);
            });

            let ingredientsCount = ingredientsArray.length;

            if(ingredientsCount === 0){
                alert("Нет введенных ингредиентов");
            }
            else{
                AxiosInstance.post('Recipe/FindRecipeByIngredients', {
                    enteringIngredientsList: ingredientsArray
                },)
                     .then(function (response) {
                        store.commit('saveResultRecipes',response.data);
                        // Логика по проверке результатов запроса рецептов
                        if (response.data.statusCode != 404)
                            vm.$router.push({path: '/showresultrecipes'});
                        else
                            alert('Рецепты, удовлетворяющие введенным ингредиентам не найдены!');
                    })
                    .catch(function (error) {
                        console.log(error);
                    });
                }

            }
        }
    };
</script>

<style scoped>

html {
    padding: 0em;
    margin: 0em;
}

body {
    margin: 0em;
    padding: 0em;
}

.content {
    display: flex;
    margin: 0em;
    padding: 0em;
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