import { createStore } from 'vuex'

export const store = createStore({
    state() {

        return {
            resultRecipe: {}        // Для хранения полученных рецептов с WebAPI
        }

    },
    getters: {
        getResultRecipes(state){
            return state.resultRecipe
        }
    },
    mutations: {
        saveResultRecipes(state, recipes){
            state.resultRecipe = recipes;
        }
    },
})