import { createStore } from 'vuex'
import { auth } from "./auth.module"

export const store = createStore({
    modules: {
        auth
    },
    state() {

        return {
            resultRecipe: {},        // Для хранения полученных рецептов с WebAPI
            newRecipeIngredients: []
        }

    },
    getters: {
        getResultRecipes(state){
            return state.resultRecipe
        },

        getIngredients(state){
            return state.newRecipeIngredients
        }
    },
    mutations: {
        saveResultRecipes(state, recipes){
            state.resultRecipe = recipes;
        },

        // Очистка списка ингредиентов при загрузке страницы
        clearIngredientList(state){
            state.newRecipeIngredients.length = 0;
        },

        // Добавление ингредиента при вводе
        addIngredient(state,ingredient){
            state.newRecipeIngredients.push(ingredient);
        },

        // Удаление ингредиента при нажатии на него
        removeIngredient(state, index){
            state.newRecipeIngredients.splice(index,1);
        },
    },
})