<template>
    <div class="content">
        <ul id="recipeList">
            <li v-for="recipe in resultRecipe" :key="recipe.name">
                <button v-on:click="showDescription(recipe.description)">{{ recipe.name }}</button>
            </li>
        </ul>

        <textarea v-model="description" readonly />

    </div>
</template>

<script lang="js">
//import { from } from 'responselike';
import { mapState } from 'vuex';

export default {

    name: 'ShowResultRecipes',

    data() {
        return{
            description: "Recipe Text",
            //recipes: []
        }
    },
    methods:{
        showDescription(description){
            this.description = description;
        }
    },
    computed: {
        ...mapState([
            'resultRecipe',
        ])
    }
}

</script>

<style>

ul {
  list-style-type:  none;
}

</style>