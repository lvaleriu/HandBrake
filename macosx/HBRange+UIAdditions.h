/*  HBRange+UIAdditions.h $

 This file is part of the HandBrake source code.
 Homepage: <http://handbrake.fr/>.
 It may be used under the terms of the GNU General Public License. */

#import <Foundation/Foundation.h>
#import "HBRange.h"

@interface HBRange (UIAdditions)

@property (nonatomic, readonly) NSArray *chapters;

@end
