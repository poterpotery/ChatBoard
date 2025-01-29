// import React from 'react';
// import { Dimensions, View, StyleSheet, Image } from 'react-native';
// import {
//   AppleButton,
//   appleAuth,
// } from '@invertase/react-native-apple-authentication';
// import { ActivityIndicator } from 'react-native';
// import { TouchableOpacity } from 'react-native';
// import { useTheme } from '../../hooks';
// async function onAppleButtonPress({ onApprove, onError, onReject,setAppleLoader, }) {

//   try {
//     setAppleLoader(true)
//     const appleAuthRequestResponse = await appleAuth.performRequest({
//       requestedOperation: appleAuth.Operation.LOGIN,
//       requestedScopes: [appleAuth.Scope.EMAIL, appleAuth.Scope.FULL_NAME],
//     });

//     // get current authentication state for user
//     // /!\ This method must be tested on a real device. On the iOS simulator it always throws an error.
//     const credentialState = await appleAuth.getCredentialStateForUser(
//       appleAuthRequestResponse.user,
//     );

//     // use credentialState response to ensure the user is authenticated
//     if (credentialState === appleAuth.State.AUTHORIZED) {
//       onApprove(appleAuthRequestResponse);
//     } else {
//       onReject(appleAuthRequestResponse);
//     }
//   } catch (error) {
//     onError(error);
//     setAppleLoader(false)
//   }
// }

// function AppleSignInbtn({ onApprove, onError, onReject, loader, setAppleLoader,disabled }) {
//   const { width } = Dimensions.get('window');

//   const {
//     Layout,
//     CustomColors,
//     Images,
//     darkMode: isDark,
//     DefaultVariables,
//   } = useTheme();
//   return (

//     <TouchableOpacity
//       activeOpacity={0.8}
//       style={styles.btnContainer}
//       disabled={disabled}
//       onPress={() => onAppleButtonPress({ onApprove, onError, onReject, setAppleLoader })}
//     >
//        {loader ? (
//         <ActivityIndicator size="small" color={'white'} />
//       ) : (
//       <>
      
//       <Image
//         source={Images.icon.apple}
//         style={{
//           width: 30,
//           height: 30,
//           resizeMode: "contain"
//         }}
//       />
//       <Text style={[styles.text, { fontWeight: 600,
//       ...Platform.select({
//         ios: {
//           marginLeft:10
//         },
        
//       }),
      
//       }]}>Continue with Apple</Text>
//       </>)}
//     </TouchableOpacity>
//   );
// }
// export default AppleSignInbtn;

// const styles = StyleSheet.create({
//   btnContainer: {
//     backgroundColor: 'pink',
//     width: '90%',
//     backgroundColor: '#000',
//     marginVertical: 10,
//     paddingVertical: 2,
//     height: 55,
//     borderRadius: 17,
//     alignItems: 'center',
//     justifyContent: 'center',
//     flexDirection: 'row',
//   },
//   icon: { height: 30, width: 30, resizeMode: 'contain', marginRight: 10 },
//   text: {
//     color: 'white',
//     fontSize: 18,
//     fontWeight: "bold",
//     marginLeft: 5,
//     ...Platform.select({
//       ios: {
//         fontWeight: 900,
//       },
//       android: {
//         paddingVertical: 25,
//       },
//     }),
//   },
// });

import { StyleSheet, Text, View } from 'react-native'
import React from 'react'

const AppleSignInbtn = () => {
  return (
    <View>
      <Text>AppleSignInbtn</Text>
    </View>
  )
}

export default AppleSignInbtn

const styles = StyleSheet.create({})