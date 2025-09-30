import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  /**@type {import('@bcwdev/auth0provider-client').Identity} */
  identity: null,
  /** @type {import('./models/Account.js').Account} user info from the database*/
  account: null,
  /** @type {import('./models/Recipe.js').Recipe[]} */
  recipes: [],
  /** @type {import('./models/Recipe.js').Recipe} */
  activeRecipe: null,
  /** @type {import('./models/Ingredient.js').Ingredient[]} */
  ingredientsForRecipe: [],
  /** @type {import('./models/FavoriteRecipe.js').FavoriteRecipe[]} */
  accountFavorites: []

})

