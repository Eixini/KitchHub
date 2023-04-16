<template>
    <div id="enterIngredients">
        <input type="text"
               ref="enterLine"
               class="form__field"
               autocomplete="off"
               placeholder="Введите ингредиент"
               v-model="ingredient"
               v-on:input="filterIngredients()"
               @keydown.enter.prevent="addIngredient()"
               @focus="modal = true"
               @focusout="desactivate"/>
        <div v-if="filterAvaibleIngredient && modal">
            <ul v-for="validResult in filterAvaibleIngredient" v-bind:key="validResult" class="ingredient-list">
                <li @click="setIngredient(validResult)" class="ingredient-list-item">{{ validResult }}</li>
            </ul>
        </div>
        <div id="ingredient-field">
            <button type="button" v-for="(ing, index) in ingredients"
                    :key="index"
                    v-on:click="deleteIngredient(index)">{{ ing }}</button>
            </div>
    </div>
</template>

<script lang="js">
    //import { ref } from 'vue';

    export default {
        name: "InputAutocomplete",
        props: {
            avaibleIngredients: Array,
        },
        data() {
            return {
                ingredient: "",
                ingredients: [],
                filterAvaibleIngredient: [],
                modal: false,
            };
        },
        methods: {
            
            // Метод для фильтрации ингредиентов
            filterIngredients(){
                var vm = this;
                vm.filterAvaibleIngredient = vm.avaibleIngredients.filter(ingredient => {
                    return ingredient.toLowerCase().startsWith(vm.ingredient.toLowerCase());
                });
            },

            // При нажатии в списке на отфильтрованный ингредиент, он вставится в строку ввода
            setIngredient(selectIngredient){
                var vm = this;     
                vm.ingredient = selectIngredient;
            },
            
            // Добавление ингредиента в список для рецепта
            addIngredient(){
                var vm = this;            
                let ingredientsArray = new Array();
                vm.ingredients.forEach(element => {
                    ingredientsArray.push(element);
                });
                // Проверка на существование ингредиента в контексте данного рецепта, во избежания дублированя
                // ВНИМАНИЕ! Возможно надо переписать, так как потенциально затратная операция
                if(ingredientsArray.includes(vm.ingredient)){
                    alert("Данный ингредиент уже имеется в поле ингредиентов!");
                    console.log("Include: " + ingredientsArray.includes(vm.ingredient));
                }
                else if(!vm.filterAvaibleIngredient.includes(vm.ingredient)){
                    console.log(vm.ingredient);
                    alert("Данный ингредиент не найден!");
                }
                else{
                    vm.ingredients.push(vm.ingredient);
                }
                
                // Очищение поля ввода ингредиентов после добавления
                vm.ingredient = "";
            },
            
            // Удаление ингредиента их создаваемого рецепта
            deleteIngredient(index) {
                var vm = this;
                vm.ingredients.splice(index,1);
            },

            // Отображение отфильрованного списка при вводе ингредиента
            desactivate(){
                var vm = this;
                setTimeout(() => vm.modal = false, 100);
            },

        }
    }
</script>