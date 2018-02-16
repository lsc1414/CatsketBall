# import <Foundation/Foundation.h>

void ShowNativeAlert( const char* title, const char* message)
{
	UIAlertView* alert = [[UIAlertView alloc] initWithTitle:[NSString stringWithUTF8String: title]

	 message:[NSString stringWithUTF8String: message]

	 delegate:nil
	 cancelButtonTitle:@"OK"

	 otherButtonTitles: nil];

	 [alert show];
 }
