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
                class="btn btn--primary btn--inside">Запросить</button>
    </div>

</template>

<script lang="js">
    
import axios from 'axios';

export default {
    
    name: 'EnteringIngredients',

    data() {
        return {
            ingredient : "",
            tags: [],
            resultRecipe: Object,
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
                        this.resultRecipe = response.data;
                        console.log('Получен ответ:');
                        console.log(this.resultRecipe);
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

</style>