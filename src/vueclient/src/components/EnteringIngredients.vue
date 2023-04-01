<template>

    <div class="content">
        <div id="enterIngredients">
            <input type="text"
                   ref="enterLine"
                   class="form__field"
                   placeholder="Введите ингредиент" />
            <button type="button"
                    v-on:click="addTag"
                    class="btn btn--primary btn--inside uppercase">Добавить</button>
        </div>

        <div id="tagField">
        <!-- Поле тегов (ингредиентов) -->
        <button type="button" v-for="(tag, index) in tags"
                :key="index"
                class="ing__tag"
                v-on:click="deleteTag(index)">{{tag}}</button>
        </div>

        <button id="sendIngredientsButton"
                type="button"
                v-on:click="sendIngredients"
                class="btn-send">Запросить</button>
    </div>

</template>

<script lang="js">
    
import router from '@/router';
import { store } from '@/store';
import axios from 'axios';

export default {
    
    name: 'EnteringIngredients',

    data() {
        return {
            ingredient : "",
            tags: [],
            resultRecipe: {},
        };
    },
    methods: {
        // Метод для получения значения с поля ввода
        inputTextChange(){
        //this.ingredient = event.target.value;
        },

        // Добавление нового тега при нажатии на кнопку
        // ЗАМЕТКА: добавить проверку на наличие введенного слова, если пусто, то метод не вызывается
        addTag(){
            console.log(this.$refs);
            if(this.$refs?.['enterLine']?.value != "")
                this.tags.push(this.$refs?.['enterLine']?.value);
            // Очищение поля ввода ингредиентов после добавления
            this.$refs.enterLine.value = "";
        },
            
        // Метод для удаления тега при нажатии на него
        deleteTag(index){
            this.tags.splice(index,1);
        },

        // Метод для отправки введенных ингредиентов на сервер для поиска рецептов
        sendIngredients(){
            let ingredientsArray = new Array();

            var vm = this;

            this.tags.forEach(element => {
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
                        //vm.resultRecipe = response.data;
                        console.log('Получен ответ:');
                        console.log(store.getters.getResultRecipes);

                        // Логика по проверке результатов запроса рецептов

                        vm.$router.push({path: '/showresultrecipes'});
                    })
                    .catch(function (error) {
                        console.log(error);
                    });
                }

            }
        }
    };

        /* Общие заметки:
        1. Возможно вместо кнопки для добавления тега можно будет использовать
        <input v-on:keyup.enter="submit">
        */

</script>

<style>

    #tagFiled {
        margin: 10px;
        word-spacing: 5px;
    }

    .ing__tag {
        background: #7f8ff4;
        color: #fff;
        border-radius: 10px;
        padding: 12px 36px;
    }

    .form__field {
        width: 360px;
        background: #fff;
        color: #a3a3a3;
        font: inherit;
        box-shadow: 0 6px 10px 0 rgba(0, 0, 0 , .1);
        border: 0;
        outline: 0;
        padding: 22px 18px;
        justify-content: center;
    }

    .uppercase {
    text-transform: uppercase;
    }

    .btn {
    display: inline-block;
    background: transparent;
    color: inherit;
    font: inherit;
    border: 0;
    outline: 0;
    padding: 0;
    transition: all 200ms ease-in;
    cursor: pointer;
    }

    .btn--primary{
        background: #7f8ff4;
        color: #fff;
        box-shadow: 0 0 10px 2px rgba(0, 0, 0, .1);
        border-radius: 2px;
        padding: 12px 36px;
    }

    .btn--primary:hover{
        background: darken(#7f8ff4, 4%);
    }

    .btn--primary:active{
        background: #7f8ff4;
        box-shadow: inset 0 0 10px 2px rgba(0, 0, 0, .2);
    }
    
    .btn--inside {
        margin-left: -96px;
    }

    .btn-send {

        display: inline-block;
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