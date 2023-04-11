<template>

    <div class="content">
        <div id="enterIngredients">
            
            <input type="text"
                   ref="enterLine"
                   class="form__field"
                   autocomplete="off"
                   placeholder="Введите ингредиент"
                   v-model="ingredient"
                   v-on:input="filterIngredients()"
                   @keydown.enter.prevent="addTag"
                   @focus="modal = true"
                   @focusout="desactivate"/>
            <div v-if="validIngredients && modal">
                <ul v-for="validResult in validIngredients" v-bind:key="validResult" class="ingredient-list">
                    <li @click="setIngredient(validResult)" class="ingredient-list-item">{{ validResult }}</li>
                </ul>
            </div>
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

    components: {
       // autocomplete
    },

    mounted(){
        var vm = this;
            // Метод для валидации данных, запрос идет к списку ингредиентов в БД
            axios.get("https://localhost:5192/Recipe/InitialGetValidIngredients")
                 .then(function(response){
                    vm.initialValidIngredients = response.data;
                    console.log(vm.initialValidIngredients);
                 })
                 .catch(function (error) {
                    console.log(error);
                 });
    },
    
    name: 'EnteringIngredients',

    data() {
        return {
            ingredient : '',
            initialValidIngredients: [],
            validIngredients: [],
            tags: [],
            resultRecipe: {},
            modal: false,
        };
    },
    methods: {
        filterIngredients(){
            var vm = this;

            vm.validIngredients = vm.initialValidIngredients.filter(ingredient => {
                return ingredient.toLowerCase().startsWith(vm.ingredient.toLowerCase());
            });
        },

        // При нажатии в списке на отфильтрованный ингредиент, он вставится в строку ввода
        setIngredient(selectIngredient){
            var vm = this;

            vm.ingredient = selectIngredient;
            vm.$refs.enterLine.value = selectIngredient;

            console.log("Select:" + selectIngredient);
        },

        // Метод для получения значения с поля ввода
        validateInput(){
            
            var vm = this;
            // Метод для валидации данных, запрос идет к списку ингредиентов в БД
            
            axios.post("https://localhost:5192/Recipe/GetValidIngredients/" + vm.ingredient)
                 .then(function(response){
                    vm.validIngredients = response.data;
                    console.log(response?.data);
                 })
                 .catch(function (error) {
                    console.log(error);
                 });
        },

        // Добавление нового тега при нажатии на кнопку
        // ЗАМЕТКА: добавить проверку на наличие введенного слова, если пусто, то метод не вызывается
        addTag(){

            var vm = this;

            let ingredientsArray = new Array();
            vm.tags.forEach(element => {
                ingredientsArray.push(element);
            });

            // Проверка на пустую строку  this.$refs?.['enterLine']?.value == "" &&


            // Проверка на существование тега, во избежания дублированя
            // ВНИМАНИЕ! Возможно надо переписать, так как потенциально затратная операция
            if(ingredientsArray.includes(this.$refs?.['enterLine']?.value)){
                alert("Данный ингредиент уже имеется в поле ингредиентов!");
                console.log("Include: " + ingredientsArray.includes(this.$refs?.['enterLine']?.value));
            }
            else if(!vm.validIngredients.includes(this.$refs?.['enterLine']?.value)){
                alert("Данный ингредиент не найден!");
            }
            else{
                this.tags.push(this.$refs?.['enterLine']?.value);
            }

            // Очищение поля ввода ингредиентов после добавления
            this.$refs.enterLine.value = "";
            this.ingredient = "";
        },
            
        // Метод для удаления тега при нажатии на него
        deleteTag(index){
            this.tags.splice(index,1);
        },

        desactivate(){
            var vm = this;
            setTimeout(() => vm.modal = false, 100);
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

                        // Отладка
                        //console.log('Получен ответ:');
                        //console.log(store.state.resultRecipe[0]);
                        //console.log(response.data);
                        //var jsonObj = JSON.parse(response.data);
                        //console.log(jsonObj);


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

        /* Общие заметки:
        1. Возможно вместо кнопки для добавления тега можно будет использовать
        <input v-on:keyup.enter="submit">
        */

</script>

<style scoped>

    #tagFiled {
        margin: 10px;
        word-spacing: 5px;
    }

    .ing__tag {
        background: #7f8ff4;
        color: #fff;
        border-radius: 5px;
        border: none;
        padding: 12px 36px;
        margin: 5px;
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

    .ingredient-list{
        width: 360px;
        background: #65a3be;
    }

    .ingredient-list-item{
        color: #fff;
        height: auto;
    }

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