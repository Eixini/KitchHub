import { createStore } from 'vuex'
import { auth } from "./auth.module"

export const store = createStore({
    modules: {
        auth
    },
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