<template>
    <div id="enterIngredients">
        <input type="text"
               ref="enterLine"
               class="form-field"
               autocomplete="off"
               placeholder="Введите ингредиент"
               v-model="ingredient"
               v-on:input="filterIngredients()"
               @keydown.enter.prevent="addIngredient()"
               @focus="modal = true"
               @focusout="desactivate"/>
        <div class="available-ingredients" v-if="filterAvaibleIngredient && modal">
            <ul v-for="validResult in filterAvaibleIngredient" v-bind:key="validResult" class="ingredient-list">
                <li @click="setIngredient(validResult)" class="ingredient-list-item">{{ validResult }}</li>
            </ul>
        </div>
        <div id="ingredient-field">
            <button type="button"
                    class="ingredient-tag"
                    v-for="(ing, index) in $store.getters.getIngredients"
                    :key="index"
                    v-on:click="deleteIngredient(index)">{{ ing }}</button>
            </div>
    </div>
</template>

<script lang="js">
    import { store } from '@/store';

    export default {
        name: "InputAutocomplete",
        props: {
            avaibleIngredients: Array,
        },

        mounted(){
            // Очистка списка ингредиентов из хранилища
            store.commit('clearIngredientList');
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
                if(store.getters.getIngredients.includes(vm.ingredient)){
                    alert("Данный ингредиент уже имеется в поле ингредиентов!");
                }
                else if(!vm.filterAvaibleIngredient.includes(vm.ingredient)){
                    console.log(vm.ingredient);
                    alert("Данный ингредиент не найден!");
                }
                else{
                    store.commit('addIngredient',vm.ingredient);
                    vm.$emit('ingredientsAdded',vm.ingredient);
                }
                
                // Очищение поля ввода ингредиентов после добавления
                vm.ingredient = "";
            },
            
            // Удаление ингредиента их создаваемого рецепта
            deleteIngredient(index) {
                var vm = this;
                store.commit('removeIngredient',index);
                vm.$emit('ingredientRemoved', index);
            },

            // Отображение отфильрованного списка при вводе ингредиента
            desactivate(){
                var vm = this;
                setTimeout(() => vm.modal = false, 100);
            },

        }
    }
</script>

<style scoped>

.form-field{

    font-family: 'Ubuntu';
    font-size: 1em;
    color: #263238;

    width: max-content;

    margin: 1em;
    margin-bottom: 0em;
}

/* class available-ingredients
    The class is responsible for the drop-down list with ingredients available for input.
*/
.available-ingredients {
    background-color: #86DFCB;

    margin-top: 0em;
}

#ingredient-field {
    display: flex;
    margin: 1em;
}

.ingredient-tag {
    font-family: 'Ubuntu';
    font-size: 1em;

    outline: none;
    border: 0.3em solid transparent; 
    border-radius: 0.3em;
    box-sizing: border-box;
    color: #263238;
    background-color: #35BFA2;
    cursor: pointer;

    margin: 0.3em;
}

/* class ingredient-list
    This class is responsible for the entire list of suggested values (dropdown list).
*/
.ingredient-list {

}

/* class ingredient-list-item
    This class is responsible for the individual element of the dropdown list for autocompletion.
*/
.ingredient-list-item {
    font-family: 'Ubuntu';
    font-size: 1em;
    color: #263238;

    display: flex;
}

.ingredient-list-item:hover {
    background-color: #35BFA2;

    display: flex;
}

</style>