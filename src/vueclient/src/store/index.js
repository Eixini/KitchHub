import { createStore } from 'vuex'

const store = createStore({
    state() {
         resultRecipe: {}

    },
    getters: {
        getResultRecipes(state){
            return state.resultRecipe
        }
    },
    mutations: {

    },
    actions: {

    }
})