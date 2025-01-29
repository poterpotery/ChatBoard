
import { apiTags } from '../apiTags';
export const getTagApi = apiTags.injectEndpoints({
    reducerPath: 'getTagApi',
    endpoints: builder => ({
        updateAccountTags: builder.mutation({
            query: tags => ({
                url: '/Auth/updateAccountTags',
                method: 'POST',
                body: tags,
            }),
            invalidatesTags: ['TAGS',],
        }),
        getAccounts: builder.query({
            query: () => '/Auth/GetAllUsers',
            providesTags: ['TAGS',],
        }),
    }),
    overrideExisting: true,
});
export const {
    useUpdateAccountTagsMutation,
    useGetAccountsQuery,
} = getTagApi;