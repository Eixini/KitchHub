<template>

    <div class="content">

        <InputAutocomplete :avaible-ingredients="avaibleIngredients"/>

        <button id="sendIngredientsButton"
                type="button"
                v-on:click="sendIngredients"
                class="btn-send">Запросить</button>
    </div>

</template>

<script lang="js">

import { store } from '@/store';
import axios from 'axios';
import InputAutocomplete from './partial/InputAutocomplete.vue';

export default {

    components: {
        InputAutocomplete,
    },

    mounted(){

        // Очистка списка ингредиентов из хранилища
        store.commit('clearIngredientList');

        var vm = this;
            // Метод для валидации данных, запрос идет к списку ингредиентов в БД
            axios.get("https://localhost:5192/Recipe/InitialGetValidIngredients")
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
                let selectIng = ingredientsArray.join(',');
                console.log(selectIng);

                axios.post("https://localhost:5192/Recipe/FindRecipeByIngredients/" + selectIng)
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

    .btn-send {


        box-sizing: border-box;
        padding: 0 13px;
        margin: 0 15px 15px 0;
        outline: none;
        border: 1px solid transparent; 
        border-radius: 3px;
        height: 32px;
        line-height: 32px;
        font-size: 14px;
        font-weight: 500;
        text-decoration: none;
        color: #fff;
        background-color: #65a3be;
        cursor: pointer;
        user-select: none;
        appearance: none;
        touch-action: manipulation;
        position: sticky;

        position: fixed;
        bottom: 0px;
        z-index:500;
        align-items: center;

    }

    .btn-send:focus-visible {

        box-shadow: 0 0 0 3px lightskyblue;

    }

    .btn-send:hover {

        border-color: transparent;
        background-color: #4986a1;
        color: #fff;

    }

    .btn-send:active {

        border-color: #6f9cbc !important;
        background-color: #367089 !important;

    }

    .btn-send:disabled {

        background-color: #558cb7;
        color: #fff;
        pointer-events: none;

    }

</style>